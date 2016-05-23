# -*- coding: utf-8 -*-
#Author:xiaohei
#CreateTime:2014-10-25
#
# The main operation entry for channel select and one thread.
#

import sys
import core
import file_utils
import apk_utils
import config_utils
import log_utils
import os
import os.path
import time
import main
import main_thread

import games
import http_utils
import argparse
import stat

def entry(isPublic, isSelectable, threadNum, appID, target):

    log_utils.info("Curr Python Version::%s", config_utils.get_py_version())

    print(u"**********所有游戏**********")
    print(u"\t appID \t\t 游戏文件夹 \t\t 游戏名称 \n\n")

    games = config_utils.getAllGames()
    if games != None and len(games) > 0:
        for ch in games:
            print(u"\t %s \t\t %s \t\t\t%s" % (ch['appID'], ch['appName'], ch['appDesc']))

    sys.stdout.write(u"请选择一个游戏(输入appID)：")
    sys.stdout.flush()

    selectedGameID = str(appID)

    game = getGameByAppID(selectedGameID, games)

    log_utils.info("current selected game is %s(%s)", game['appName'], game['appDesc'])

    if isSelectable:
        return main(game, isPublic, target)
    else:
        return main_thread.main(game, isPublic, threadNum)


def main(game, isPublic, target):
    appName = game['appName']
    channels = config_utils.getAllChannels(appName, isPublic)
    if channels is None or len(channels) == 0:
        print("没有任何可以打包的渠道")
        return 3

    selected = []

    if target == '*':
        selected = channels
    else:
        for t in target.split(','):
            t = t.strip()
            matchChannels = [c for c in channels if c['name'].lower() == t.lower()]
            if len(matchChannels) > 0:
                selected.append(matchChannels[0])

    clen = len(selected)
    log_utils.info("now hava %s channels to package...", clen)
    baseApkPath = file_utils.getFullPath('games/'+game['appName']+'/u8.apk')
    log_utils.info("the base apk file is : %s", baseApkPath)

    if not os.path.exists(baseApkPath):
        log_utils.error('the base apk file is not exists, must named with u8.apk')
        return 2

    sucNum = 0
    failNum = 0
    
    for channel in selected:
        ret = core.pack(game, channel, baseApkPath, isPublic)
        if ret:
            exit(1)
            failNum = failNum + 1
        else:
            sucNum = sucNum + 1

    log_utils.info("<< success num:%s; failed num:%s >>", sucNum, failNum)
    if failNum > 0:
        log_utils.error("<< all done with error >>")
    else:
        log_utils.info("<< all nice done >>")
    return 0


def getGameByAppID(appID, games):

    if games == None or len(games) <= 0:
        return None

    for game in games:
        if game['appID'] == appID:
            return game

    return None


if __name__ == "__main__":

    parser = argparse.ArgumentParser(u"U8SDK 打包工具")
    parser.add_argument('-r', '--release', help=u"标记为正式包，非正式包会在包名后面加上.debug",action="store_true", dest="release", default=False)
    parser.add_argument('-s', '--select', help=u"让用户自己选择需要打包的渠道。否则将会打出所有渠道包", action="store_true", dest="selectable", default=False)
    parser.add_argument('-t', '--thread', help=u"全部打包时的打包线程数量", action='store', dest="threadNum", type=int, default=1)
    parser.add_argument('--version', help=u"查看当前使用的U8SDK版本", action='version', version='v2.0')
    parser.add_argument('--appID', help=u"打包appid", action='store', dest="appID")
    parser.add_argument('--target', help=u"打包appid", action='store', dest="target")
    
    args = parser.parse_args()

    print(args)
    print(args.appID)
    print(args.target)

    ret = entry(args.release, args.selectable, args.threadNum, args.appID, args.target);
    exit(ret)
