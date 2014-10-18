using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Update
{
	/// <summary>
	/// 画面への表示を行います。
	/// </summary>
	interface DisplayArea
	{
		/// <summary>
		/// 表示内容を更新します。
		/// </summary>
		/// <param name="image">表示するイメージ</param>
		void updateArea(Image image);
	}
}
