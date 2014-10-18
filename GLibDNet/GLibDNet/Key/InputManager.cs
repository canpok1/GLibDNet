using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GLibDNet.Util;

namespace GLibDNet.Key
{
	/// <summary>
	/// マウスとキーボードの入力情報を管理するクラス。
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class InputManager
	{
		/// <summary>
		/// 使用できるキー
		/// </summary>
		public enum KeyCodeEnum
		{
			ML,	// マウスの左ボタン
			MR,	// マウスの右ボタン

			UP, // 矢印キーの上
			DOWN,	// 矢印キーの下
			LEFT,	// 矢印キーの左
			RIGHT,	// 矢印キーの右

			ESC,	// ESCキー
			ENTER,	// ENTERキー
			SPACE,	// SPACEキー

			UNUSED	// 使用しないキー
		}


		/// <summary>
		/// マウスの回転方向
		/// </summary>
		public enum RotationDirection
		{
			OVER_SPIN,	// 順回転
			BACK_SPIN,	// 逆回転
			STOP	// 停止
		}

		/// <summary>
		/// インスタンス
		/// </summary>
		private static InputManager singleton = new InputManager();

		/// <summary>
		/// マウスホイール回転数の最大値
		/// </summary>
		public const int MAX_WHEEL_ROTATION = 1000;

		/// <summary>
		/// マウスカーソルの座標
		/// </summary>
		private Point mousePoint;

		/// <summary>
		/// ノーマルモードのキー
		/// </summary>
		private Dictionary<KeyCodeEnum, ActionKey> normalModeKeys;

		/// <summary>
		/// ワンタイムモードのキー
		/// </summary>
		private Dictionary<KeyCodeEnum, ActionKey> oneTimeModeKeys;

		/// <summary>
		/// ホイールの回転数
		/// 0からMAX_WHEEL_ROTATION-1までの範囲になります。
		/// </summary>
		private int wheelRotation;

		/// <summary>
		/// ホイールの回転方向
		/// </summary>
		private RotationDirection rotationDirection;

		/// <summary>
		/// ログ記録インスタンス
		/// </summary>
		private Logger logger;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private InputManager()
		{
			this.logger = LoggerGetter.getInstance().getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

			this.mousePoint = new Point(0, 0);

			this.normalModeKeys = new Dictionary<KeyCodeEnum, ActionKey>();
			this.oneTimeModeKeys = new Dictionary<KeyCodeEnum, ActionKey>();

			// 各キーのインスタンスを生成
			foreach( KeyCodeEnum key in Enum.GetValues(typeof(KeyCodeEnum)) )
			{
				if (key != KeyCodeEnum.UNUSED)
				{
					this.normalModeKeys.Add(key, new ActionKey(ActionKey.NORMAL_MODE));
					this.oneTimeModeKeys.Add(key, new ActionKey(ActionKey.ONE_TIME_MODE));
				}
			}

			this.initialize();

		}


		/// <summary>
		/// インスタンスを取得します
		/// </summary>
		/// <returns>インスタンス</returns>
		public static InputManager getInstance()
		{
			return InputManager.singleton;
		}


		/// <summary>
		/// 全てのキー状態、ホイール回転数を初期化します
		/// </summary>
		public void initialize()
		{
			foreach (ActionKey key in normalModeKeys.Values)
			{
				key.reset();
			}

			foreach (ActionKey key in oneTimeModeKeys.Values)
			{
				key.reset();
			}

			this.wheelRotation = 0;
			this.rotationDirection = RotationDirection.STOP;
		}


		/// <summary>
		/// ボタンが押されたときの状態を設定します
		/// </summary>
		/// <param name="keyCode">押されたボタン</param>
		internal void pressed(KeyCodeEnum keyCode)
		{
			try
			{
				if (keyCode == KeyCodeEnum.UNUSED)
				{
					this.logger.debug(
						"未対応のキーが押されました。");
					return;
				}

				// ノーマルモードのキー状態を更新
				ActionKey normalKey = this.normalModeKeys[keyCode];
				normalKey.press();

				// ワンタイムモードのキー状態を更新
				ActionKey oneTimeKey = this.oneTimeModeKeys[keyCode];
				oneTimeKey.press();

				this.logger.debug(
					Enum.GetName(typeof(KeyCodeEnum), keyCode) + "が押されました。" );
			}
			catch (KeyNotFoundException)
			{
				this.logger.warn(
					Enum.GetName(typeof(KeyCodeEnum), keyCode) + "が押されましたが状態は未管理です。" );
			}
		}


		/// <summary>
		/// ボタンが離されたときの状態を設定します
		/// </summary>
		/// <param name="keyCode"></param>
		internal void released(KeyCodeEnum keyCode)
		{
			try
			{
				if (keyCode == KeyCodeEnum.UNUSED)
				{
					this.logger.debug(
						"未対応のキーが離されました。");
					return;
				}

				// ノーマルモードのキー状態を更新
				ActionKey normalKey = this.normalModeKeys[keyCode];
				normalKey.release();

				// ワンタイムモードのキー状態を更新
				ActionKey oneTimeKey = this.oneTimeModeKeys[keyCode];
				oneTimeKey.release();

				this.logger.debug(
					Enum.GetName(typeof(KeyCodeEnum), keyCode) + "が離されました。" );
			}
			catch (KeyNotFoundException)
			{
				this.logger.warn(
					Enum.GetName(typeof(KeyCodeEnum), keyCode) + "が離されましたが状態は未管理です。" );
			}
		}


		/// <summary>
		/// マウスカーソルの位置を更新します
		/// </summary>
		/// <param name="p">マウスカーソルの位置</param>
		internal void mouseMoved(Point p)
		{
			// 引数チェック
			if (p == null)
			{
				throw new ArgumentNullException("p", "nullにすることはできません。");
			}

			this.mousePoint.X = p.X;
			this.mousePoint.Y = p.Y;
		}


		/// <summary>
		/// マウスホイールの回転数を設定します。
		/// </summary>
		/// <param name="r">回転数</param>
		internal void setWheelRotation(int r)
		{
			int sub = this.wheelRotation - r;
			// 回転数導出
			if( sub > 0 )
			{
				this.rotationDirection = RotationDirection.OVER_SPIN;
			}
			else if (sub < 0)
			{
				this.rotationDirection = RotationDirection.BACK_SPIN;
			}
			else
			{
				this.rotationDirection = RotationDirection.STOP;
			}

			int n = r;

			// 0以上の値に直す
			while (n < 0)
			{
				n += MAX_WHEEL_ROTATION;
			}

			//MAX_WHEEL_ROTATION未満の値に直す
			while (n >= MAX_WHEEL_ROTATION)
			{
				n -= MAX_WHEEL_ROTATION;
			}

			this.wheelRotation = n;
		}


		/// <summary>
		/// ノーマルモードのキーが押されているかを判定
		/// </summary>
		/// <param name="keyCode">キーコード</param>
		/// <returns>true:押されている false:押されていない</returns>
		/// <exception cref="ArgumentException">対応するキーがない場合に発生</exception>
		public Boolean isNormalModeKeyPressed(KeyCodeEnum keyCode)
		{
			try
			{
				ActionKey key = this.normalModeKeys[keyCode];
				Boolean result = key.isPressed();
				return result;
			}
			catch (KeyNotFoundException)
			{
				throw new ArgumentException( "keyCode", "対応するキーがありません。" );
			}
		}


		/// <summary>
		/// ワンタイムモードのキーが押されているかを判定
		/// </summary>
		/// <param name="keyCode">キーコード</param>
		/// <returns>true:押されている false:押されていない</returns>
		/// <exception cref="ArgumentException">対応するキーがない場合に発生</exception>
		public Boolean isOneTimeModeKeyPressed(KeyCodeEnum keyCode)
		{
			try
			{
				ActionKey key = this.oneTimeModeKeys[keyCode];
				Boolean result = key.isPressed();
				return result;
			}
			catch (KeyNotFoundException)
			{
				throw new ArgumentException("keyCode", "対応するキーがありません。");
			}
		}


		/// <summary>
		/// ホイールの回転数を取得します。
		/// ホイールを手前に回すと数値が増え、奥に回すと数値が減ります。
		/// </summary>
		/// <returns>ホイールの回転数。0からMAX_WHEEL_ROTATION-1の範囲です。</returns>
		public int getWheelRotation()
		{
			return this.wheelRotation;
		}


		/// <summary>
		/// ホイールの回転方向を取得します。
		/// </summary>
		/// <returns>回転方向</returns>
		public RotationDirection getRotationDirection()
		{
			return this.rotationDirection;
		}


		/// <summary>
		/// マウスカーソルの位置を取得します。
		/// </summary>
		/// <returns>マウスカーソルの位置座標</returns>
		public Point getMousePoint()
		{
			Point result = new Point();
			result.X = this.mousePoint.X;
			result.Y = this.mousePoint.Y;
			return result;
		}
	}
}
