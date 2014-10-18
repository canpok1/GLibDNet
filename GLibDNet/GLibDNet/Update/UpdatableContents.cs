using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLibDNet.Update
{
	/// <summary>
	/// <newpara>
	/// �Q�[�����[�v�ōX�V���s�����߂̃C���^�[�t�F�[�X�ł��B
	/// </newpara>
	/// </summary>
	[CLSCompliantAttribute(true)]
	public abstract class UpdatableContents : IComparable
	{
		/// <summary>
		/// �X�V�J�e�S�����擾���܂��B
		/// �����J�e�S���̂��͓̂����X���b�h���ōX�V����܂��B
		/// </summary>
		public abstract Byte getCategory();

		/// <summary>
		/// �X�V���x�����擾���܂��B
		/// �X�V���x�������������̂��珇�ԂɍX�V���s���܂��B
		/// </summary>
		public abstract Byte getUpdateLevel();

		/// <summary>
		/// ����X�V���s���܂��B
		/// �����J�e�S���̃C���X�^���X�͓���X���b�h���ōX�V���܂��B
		/// �X���b�h���ł͍X�V���x�����������C���X�^���X���珇�ɍX�V����܂��B
		/// �����X�V���x���̏ꍇ�A��ɓo�^���ꂽ�C���X�^���X����X�V����܂��B
		/// </summary>
		public abstract void parallelUpdate();

		/// <summary>
		/// ���ԂɍX�V���s���܂��B
		/// �S�J�e�S���̃C���X�^���X�͓���X���b�h���ōX�V���܂��B
		/// </summary>
		public abstract UpdatedStateEnum syncronousUpdate();

		/// <summary>
		/// �R���e���c�̃��\�[�X��������܂��B
		/// ���\�[�X�̎Q�Ƃ��폜���A�K�x�[�W�R���N�V�����̑Ώۂɂ��܂��B
		/// </summary>
		public abstract void cleanup();

		/// <summary>
		/// �X�V���x���̔�r���s���܂��B
		/// </summary>
		/// <param name="obj">��r�Ώۂ̃C���X�^���X</param>
		/// <returns>��:��r�Ώۂ��傫�� ��:��r�Ώۂ�菬���� 0:��r�ΏۂƓ���</returns>
		public int CompareTo(Object obj)
		{
			// �I�u�W�F�N�g�̃`�F�b�N
			if( obj == null )
			{
				throw new ArgumentNullException("obj", "null�ɂ͂ł��܂���B");
			}
			if ( (obj is UpdatableContents) == false )
			{
				throw new ArgumentException("obj", "UpdatableContents�̃C���X�^���X�łȂ���΂Ȃ�܂���B");
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
