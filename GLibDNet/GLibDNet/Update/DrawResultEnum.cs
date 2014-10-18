using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	/// <summary>
	/// 描画の結果を表す列挙です。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public enum DrawResultEnum
	{
		ONCE,			// 一度だけ描画
		CONTINUE,		// 続けて描画
	}
}
