using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

    public class AppConst {
		public const bool DebugMode = false;                        //调试模式-用于内部测试

        /// <summary>
        /// 如果想删掉框架自带的例子，那这个例子模式必须要
        /// 关闭，否则会出现一些错误。
        /// </summary>
		public const bool ExampleMode = true;                       //例子模式  zys显示模板面板 通讯例子;

        /// <summary>
        /// 如果开启更新模式，前提必须启动框架自带服务器端。
        /// 否则就需要自己将StreamingAssets里面的所有内容
        /// 复制到自己的Webserver上面，并修改下面的WebUrl。
        /// </summary>
		public const bool UpdateMode = false;                      //更新模式-默认关闭 
        public const bool LuaByteMode = false;                     //Lua字节码模式-默认关闭 
		// zys 关闭情况下不会读取boudle ，直接读取lua项目代码
		//false  情况下 源文件 全部打包进入 StreamingAssets 文件夹下;
		//true 的情况下，通过字节流打包成u3d文件进入;
		public const bool LuaBundleMode = false;                   //Lua代码AssetBundle模式-默认关闭 

        public const int TimerInterval = 1;
        public const int GameFrameRate = 60;                       //游戏帧频

        public const string AppName = "DarkRun";               //应用程序名称
        public const string LuaTempDir = "Lua/";                    //临时目录
        public const string ExtName = ".unity3d";                   //资源扩展名
        public const string AppPrefix = AppName + "_";              //应用程序前缀
		public const string WebUrl = "http://10.0.4.149/StreamingAssets/";      //测试更新地址

        public static string UserId = string.Empty;                 //用户ID
		//具体信息将会在lua中设置;
        public static int SocketPort = 0;                           //Socket服务器端口
		public static string SocketAddress = string.Empty;          //Socket服务器地址

        public static string FrameworkRoot {
            get {
                return Application.dataPath + "/" + AppName;
            }
        }
    }
