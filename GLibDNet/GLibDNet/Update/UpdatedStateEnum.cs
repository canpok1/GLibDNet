using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	/// <summary>
	/// <newpara> 更新後の状態を表す列挙です。</newpara>
	/// </summary>
	[CLSCompliantAttribute(true)]
	public enum UpdatedStateEnum
	{
		Continue,	// 更新後も更新対象のまま
		Remove	// 描画後に更新対象から削除
	}
}
