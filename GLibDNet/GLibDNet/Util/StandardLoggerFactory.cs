using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Key;
using GLibDNet.Update;
using GLibDNet.Sound;

namespace GLibDNet.Util
{
	/// <summary>
	/// コンソールへ出力するLoggerを生成するクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class StandardLoggerFactory : LoggerFactory
	{
		/// <summary>
		/// Loggerを生成します。
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Logger create(Type type)
		{
			ConsoleLogger.LoggingLevel level = ConsoleLogger.LoggingLevel.INFO;

			List<Type> debugList = new List<Type>();
			debugList.Add(typeof(SoundManager));

			foreach (Type check in debugList)
			{
				if (type.Equals(check) == true)
				{
					level = ConsoleLogger.LoggingLevel.DEBUG;
				}
			}

			ConsoleLogger logger 
				= new ConsoleLogger(
						type,
						level );
			return logger;
		}
	}
}
