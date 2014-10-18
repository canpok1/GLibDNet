using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Key
{
	/// <summary>
	/// キーの状態を表すクラスです。
	/// </summary>
	internal class ActionKey
	{
		/// <summary>
		/// 押されている間は常にtrueを返すモード
		/// </summary>
		public const Byte NORMAL_MODE = 0;
		/// <summary>
		/// 押されている間、最初の一度だけtrueを返すモード
		/// </summary>
		public const Byte ONE_TIME_MODE = 1;

		/// <summary>
		/// キーが離されている状態
		/// </summary>
		private const Byte STATE_RELEASED = 0;
		/// <summary>
		/// キーが押されていて、trueを返す状態
		/// </summary>
		private const Byte STATE_PRESSED = 1;
		/// <summary>
		/// キーが押されていて、falseを返す状態
		/// </summary>
		private const Byte STATE_WAITING_FOR_RELEASE = 2;

		/// <summary>
		/// キーのモード
		/// </summary>
		private Byte mode;
		/// <summary>
		/// キーの状態
		/// </summary>
		private Byte state;


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="mode">キーのモード</param>
		public ActionKey(Byte mode)
		{
			// 引数チェック
			if (mode != NORMAL_MODE && mode != ONE_TIME_MODE)
			{
				throw new ArgumentOutOfRangeException("mode", "対応するモードがありません。");
			}

			this.mode = mode;
			this.reset();
		}


		/// <summary>
		/// キーの状態をリセットします。
		/// </summary>
		public void reset()
		{
			this.state = ActionKey.STATE_RELEASED;
		}


		/// <summary>
		/// キーが押されたときに呼び出す。
		/// </summary>
		public void press()
		{
			if (state != ActionKey.STATE_WAITING_FOR_RELEASE)
			{
				this.state = ActionKey.STATE_PRESSED;
			}
		}


		/// <summary>
		/// キーが離されたときに呼び出す。
		/// </summary>
		public void release()
		{
			this.state = ActionKey.STATE_RELEASED;
		}


		/// <summary>
		/// キーが押されたか判定する。
		/// </summary>
		/// <returns>true:押されている false:押されていない</returns>
		public Boolean isPressed()
		{
			if (this.state == ActionKey.STATE_PRESSED)
			{
				if (this.mode == ActionKey.ONE_TIME_MODE)
				{
					// 最初の一回だけtrue
					this.state = ActionKey.STATE_WAITING_FOR_RELEASE;
				}
				return true;
			}
			return false;
		}
	}
}
