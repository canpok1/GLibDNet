using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Drawing.Shapes
{
	/// <summary>
	/// 図形
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class ShapeUtil
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private ShapeUtil()
		{
		}

		/// <summary>
		/// 図形同士の衝突判定
		/// </summary>
		/// <param name="shape1">四角形</param>
		/// <param name="shape2">四角形</param>
		/// <returns>true:衝突している false:衝突していない</returns>
		public static Boolean CollisionCheck(Rectangle shape1, Rectangle shape2)
		{
			// 引数チェック
			if (shape1 == null)
			{
				throw new ArgumentNullException("shape1", "Nullにできません。");
			}
			if (shape2 == null)
			{
				throw new ArgumentNullException("shape2", "Nullにできません。");
			}

			Int32 left1 = shape1.getPoint().getIntegerX();
			Int32 right1 = left1 + shape1.getSize().Width;
			Int32 top1 = shape1.getPoint().getIntegerY();
			Int32 bottom1 = top1 + shape1.getSize().Height;

			Int32 left2 = shape2.getPoint().getIntegerX();
			Int32 right2 = left2 + shape2.getSize().Width;
			Int32 top2 = shape2.getPoint().getIntegerY();
			Int32 bottom2 = top2 + shape2.getSize().Height;

			if (left1 < right2
				&& left2 < right1
				&& top2 < bottom1
				&& top1 < bottom2)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 図形同士の衝突判定
		/// </summary>
		/// <param name="shape1">四角形</param>
		/// <param name="shape2">円</param>
		/// <returns>true:衝突している false:衝突していない</returns>
		public static Boolean CollistionCheck(Rectangle shape1, Circle shape2)
		{
			// 引数チェック
			if (shape1 == null)
			{
				throw new ArgumentNullException("shape1", "Nullにできません。");
			}
			if (shape2 == null)
			{
				throw new ArgumentNullException("shape2", "Nullにできません。");
			}

			// TODO 実装


			return false;
		}

		/// <summary>
		/// 図形同士の衝突判定
		/// </summary>
		/// <param name="shape1">円</param>
		/// <param name="shape2">円</param>
		/// <returns>true:衝突している false:衝突していない</returns>
		public static Boolean CollisionCheck(Circle shape1, Circle shape2)
		{
			// 引数チェック
			if (shape1 == null)
			{
				throw new ArgumentNullException("shape1", "Nullにできません。");
			}
			if (shape2 == null)
			{
				throw new ArgumentNullException("shape2", "Nullにできません。");
			}

			// TODO 実装

			return false;
		}
	}
}
