using System;
using System.Collections.Generic;

namespace TsiU
{
	public interface TILoggerListener
	{
		void log(string msg);
	};

	public class TLogger : TStaticHelperBase<TLogger>
	{
		//-------------------------------------------
		public enum LOGGER_CHANNEL {
			DEFAULT = 0,
			WARNING,
			INFO,
			TODO,
			ERROR,
			DEBUG,
			PROFILE,
			NUM
		};
		public enum LOGGER_REDIRECTION {
			CONSOLE = 0,
			STRING,
			FILE
		};
        //------------------------------------------
        static public void DEBUG(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.DEBUG, false);
        }
        static public void WARNING(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.WARNING, false);
        }
        static public void INFO(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.INFO, false);
        }
        static public void TODO(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.TODO, false);
        }
        static public void ERROR(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.ERROR, false);
        }
        static public void PROFILE(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.PROFILE, false);
        }
        //-------------------------------------------
		private bool[] _enabledChannel;
		private List<TILoggerListener> _listeners;
		//-------------------------------------------
		protected override void onInit()
		{
			_enabledChannel = new bool[(int)LOGGER_CHANNEL.NUM];
			for(int i = 0; i < _enabledChannel.Length; ++i){
				_enabledChannel[i] = true;
			}
            _listeners = new List<TILoggerListener>();
		}
		//-------------------------------------------
		public void EnableChannel(LOGGER_CHANNEL channel, bool isEnabled)
		{
			if(!hasInited){
				return;
			}
			if(channel == LOGGER_CHANNEL.NUM){
				return;
			}
			_enabledChannel[(int)channel] = isEnabled;
		}
		public void AddLogListener(TILoggerListener listener)
		{
			if(!hasInited){
				return;
			}
			_listeners.Add(listener);
		}
		public void Log(string msg, LOGGER_CHANNEL channel, bool simpleMode)
		{
			if(!hasInited){
				return;
			}
			if(_enabledChannel[(int)channel] == false){
				return;
			}
			string outputMsg;
			if(simpleMode){
				outputMsg = msg;
			} else {
				outputMsg = 
					"(at " + DateTime.Now.ToString() + ")" + 
					"[" + channel.ToString() + "]" + msg;
			}
			foreach(TILoggerListener listener in _listeners){
				listener.log(outputMsg);
			}
			Console.WriteLine(outputMsg);
		}
	};
}

