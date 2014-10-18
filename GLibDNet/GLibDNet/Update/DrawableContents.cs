using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Update
{
	/// <summary>
	/// ゲームループ内で描画するためのインターフェースです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public interface DrawableContents
	{
		/// <summary>
		/// 描画します。
		/// </summary>
		DrawResultEnum drawMyself(Graphics g);
	}
}
