using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	/// <summary>
	/// <newpara>
	/// ゲームループで更新を行うためのインターフェースです。
	/// </newpara>
	/// </summary>
	[CLSCompliantAttribute(true)]
	public abstract class UpdatableContents : IComparable
	{
		/// <summary>
		/// 更新カテゴリを取得します。
		/// 同じカテゴリのものは同じスレッド内で更新されます。
		/// </summary>
		public abstract Byte getCategory();

		/// <summary>
		/// 更新レベルを取得します。
		/// 更新レベルが小さいものから順番に更新が行われます。
		/// </summary>
		public abstract Byte getUpdateLevel();

		/// <summary>
		/// 並列更新を行います。
		/// 同じカテゴリのインスタンスは同一スレッド内で更新します。
		/// スレッド内では更新レベルが小さいインスタンスから順に更新されます。
		/// 同じ更新レベルの場合、先に登録されたインスタンスから更新されます。
		/// </summary>
		public abstract void parallelUpdate();

		/// <summary>
		/// 順番に更新を行います。
		/// 全カテゴリのインスタンスは同一スレッド内で更新します。
		/// </summary>
		public abstract UpdatedStateEnum syncronousUpdate();

		/// <summary>
		/// コンテンツのリソースを解放します。
		/// リソースの参照を削除し、ガベージコレクションの対象にします。
		/// </summary>
		public abstract void cleanup();

		/// <summary>
		/// 更新レベルの比較を行います。
		/// </summary>
		/// <param name="obj">比較対象のインスタンス</param>
		/// <returns>正:比較対象より大きい 負:比較対象より小さい 0:比較対象と同じ</returns>
		public int CompareTo(Object obj)
		{
			// オブジェクトのチェック
			if( obj == null )
			{
				throw new ArgumentNullException("obj", "nullにはできません。");
			}
			if ( (obj is UpdatableContents) == false )
			{
				throw new ArgumentException("obj", "UpdatableContentsのインスタンスでなければなりません。");
			}

			UpdatableContents content = (UpdatableContents)obj;

			if( this.getUpdateLevel() < content.getUpdateLevel() )
			{
				return -1;
			}
			else if (this.getUpdateLevel() > content.getUpdateLevel())
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
	}
}
