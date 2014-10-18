using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Drawing.Elements
{
	/// <summary>
	/// 移動する要素を表すクラスです。
	/// </summary>
	public abstract class MovableGameElement : BaseGameElement
	{
		/// <summary>
		/// 移動速度
		/// </summary>
		private Velocity velocity;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MovableGameElement()
			: base()
		{
			this.velocity = new Velocity(0.0F, 0.0F);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="p">座標</param>
		/// <param name="s">大きさ</param>
		/// <param name="v">速度</param>
		public MovableGameElement(Position p, Size s, Velocity v)
			: base(p, s)
		{
			this.velocity = new Velocity(v);
		}

		/// <summary>
		/// 速度を設定します。
		/// </summary>
		/// <param name="vx">X方向の速度</param>
		/// <param name="vy">Y方向の速度</param>
		public void setVelocity(Single vx, Single vy)
		{
			this.velocity.set(vx, vy);
		}

		/// <summary>
		/// 速度を設定します。
		/// </summary>
		/// <param name="v">速度</param>
		public void setVelocity(Velocity v)
		{
			// 引数チェック
			if (v == null)
			{
				throw new ArgumentNullException("v", "Nullにはできません。");
			}
			this.velocity.set(v.getVX(), v.getVY());
		}

		/// <summary>
		/// 速度を取得します。
		/// </summary>
		/// <returns>速度</returns>
		public Velocity getVelocity()
		{
			return new Velocity(this.velocity);
		}

		/// <summary>
		/// 移動します。
		/// </summary>
		public virtual void moveForward()
		{
			Single x = this.getPoint().getX() + this.velocity.getVX();
			Single y = this.getPoint().getY() + this.velocity.getVY();

			this.setPoint(x, y);
		}

		/// <summary>
		/// 移動します。
		/// </summary>
		public virtual void moveBack()
		{
			Single x = this.getPoint().getX() - this.velocity.getVX();
			Single y = this.getPoint().getY() - this.velocity.getVY();

			this.setPoint(x, y);
		}
	}
}
