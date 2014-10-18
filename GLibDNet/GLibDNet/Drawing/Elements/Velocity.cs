using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Drawing.Elements
{
	/// <summary>
	/// 速度を表すクラスです。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class Velocity
	{
		/// <summary>
		/// 速度
		/// </summary>
		private PointF velocity;

		/// <summary>
		/// 速度を生成します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		/// <param name="vy">Y方向の速度</param>
		public Velocity(Single vx, Single vy)
		{
			this.velocity = new PointF(vx, vy);
		}

		/// <summary>
		/// 速度を元に新規に速度を生成します。
		/// </summary>
		/// <param name="obj">元になる速度</param>
		public Velocity(Velocity obj)
		{
			// 引数チェック
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "Nullにできません。");
			}

			this.velocity = new PointF(obj.getVX(), obj.getVY());
		}

		/// <summary>
		/// X方向の速度を取得します。
		/// </summary>
		/// <returns>X方向の速度</returns>
		public Single getVX()
		{
			return this.velocity.X;
		}

		/// <summary>
		/// Y方向の速度を取得します。
		/// </summary>
		/// <returns>Y方向の速度</returns>
		public Single getVY()
		{
			return this.velocity.Y;
		}

		/// <summary>
		/// X方向の速度を取得します。
		/// 四捨五入した値となります。
		/// </summary>
		/// <returns>X方向の速度</returns>
		public Int32 getIntegerVX()
		{
			Int32 vx = (Int32)Math.Round(this.velocity.X);
			return vx;
		}

		/// <summary>
		/// Y方向の速度を取得します。
		/// 四捨五入した値となります。
		/// </summary>
		/// <returns>Y方向の速度</returns>
		public Int32 getIntegerVY()
		{
			Int32 vy = (Int32)Math.Round(this.velocity.Y);
			return vy;
		}

		/// <summary>
		/// X方向の速度を設定します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		public void setVX(Single vx)
		{
			this.velocity.X = vx;
		}

		/// <summary>
		/// X方向の速度を設定します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		public void setVX(Int32 vx)
		{
			this.velocity.X = (Single)vx;
		}

		/// <summary>
		/// Y方向の速度を設定します。
		/// </summary>
		/// <param name="vy">Y方向の速度</param>
		public void setVY(Single vy)
		{
			this.velocity.Y = vy;
		}

		/// <summary>
		/// Y方向の速度を設定します。
		/// </summary>
		/// <param name="vy">Y方向の速度</param>
		public void setVY(Int32 vy)
		{
			this.velocity.Y = (Single)vy;
		}

		/// <summary>
		/// 速度を設定します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		/// <param name="vy">Y方向の速度</param>
		public void set(Single vx, Single vy)
		{
			this.setVX(vx);
			this.setVY(vy);
		}

		/// <summary>
		/// 速度を設定します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		/// <param name="vy">Y方向の速度</param>
		public void set(Int32 vx, Int32 vy)
		{
			this.setVX(vx);
			this.setVY(vy);
		}
	}
}
