using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GLibDNet.Update
{
	/// <summary>
	/// �Q�[�����[�v���ŕ`�悷�邽�߂̃C���^�[�t�F�[�X�ł��B
	/// </summary>
	[CLSCompliantAttribute(true)]
	public interface DrawableContents
	{
		/// <summary>
		/// �`�悵�܂��B
		/// </summary>
		DrawResultEnum drawMyself(Graphics g);
	}
}
