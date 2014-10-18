using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GLibDNet.Update;
using System.Windows.Forms;
using GLibDNet.Util;
using System.Threading;
using GLibDNet.Sound;

[assembly : CLSCompliant(true)]
namespace GLibDNet
{
	/// <summary>
	/// 
	/// </summary>
	[CLSCompliantAttribute(true)]
	public class GLib
    {
        /// <summary>
        /// �N���X�̃C���X�^���X�ł��B
        /// </summary>
		private static GLib singleton = null;

		/// <summary>
		/// �Q�[���̃p�����[�^�ł��B
		/// </summary>
		private GameProperties properties;

		/// <summary>
		/// �Q�[����ʂ�`�悷��t���[���ł��B
		/// </summary>
		private GameFrame frame;

		/// <summary>
		/// �V���b�g�_�E�������Ď��X���b�h�ł��B
		/// </summary>
		private Thread shutdownThread;

		/// <summary>
		/// �\���̈�̕\����Ԃł��B
		/// true:�\�� false:��\��
		/// </summary>
		private Boolean displayAreaActivation;

		/// <summary>
		/// ���K�[�ł��B
		/// </summary>
		private Logger logger;

        /// <summary>
        /// <newpara>�C���X�^���X�𐶐����܂��B</newpara>
        /// </summary>
		private GLib( LoggerFactory loggerFactory )
		{
			LoggerGetter lg = LoggerGetter.getInstance();
			if (loggerFactory != null)
			{
				lg.setFactory(loggerFactory);
			}
			this.logger = lg.getLogger(
							System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

			this.properties = null;
			this.frame = null;
			this.shutdownThread = null;
			this.displayAreaActivation = false;
		}

        /// <summary>
        /// <newpara>�C���X�^���X���擾���܂��B</newpara>
        /// </summary>
		/// <param name="loggerFactory">LoggerFactory</param>
        /// <returns>�C���X�^���X</returns>
		public static GLib getInstance( LoggerFactory loggerFactory )
		{
			if (GLib.singleton == null)
			{
				GLib.singleton = new GLib(loggerFactory);
			}

			return GLib.singleton;
		}


		/// <summary>
		/// �C���X�^���X���擾���܂��B
		/// </summary>
		/// <returns>�C���X�^���X</returns>
		public static GLib getInstance()
		{
			return GLib.singleton;
		}

		/// <summary>
		/// �Q�[�����~���܂��B
		/// </summary>
		public void stop()
		{
			if (this.shutdownThread == null)
			{
				this.shutdownThread = new Thread(new ThreadStart(this.shutdownProcess));
				this.shutdownThread.Name = "ShutdownThread";

				this.logger.debug("�V���b�g�_�E���X���b�h����");

				this.shutdownThread.Start();
			}
			else
			{
				this.logger.debug(
					"�V���b�g�_�E���̏����͋N�����ł��B");
			}
		}


		/// <summary>
		/// �Q�[���̒�~�����ł��B
		/// </summary>
		private void shutdownProcess()
		{
			this.logger.info(
				"��~�����J�n");

			GameLoopGenerator.getInstance().stop();
			AnimationGenerator.getInstance().stop();
			SoundManager.getInstance().clear();

			if (this.displayAreaActivation == true)
			{
				// �t���[�����J���Ă��������
				if (this.frame != null)
				{
					this.frame.BeginInvoke(
						(MethodInvoker)delegate
						{
							this.frame.Close();
							this.frame.Dispose();
						}
					);
				}
				this.displayAreaActivation = false;
			}

			this.shutdownThread = null;
			this.logger.info(
				"��~�����I��");
		}

		/// <summary>
		/// �Q�[����ʕ`��p�t���[�����擾���܂��B
		/// ���̃��\�b�h���ĂԂ��ƂŃA�j���[�V�����̐������J�n����܂��B
		/// </summary>
		/// <returns>�t���[��</returns>
		public Form getFrame( GameProperties properties, SceneParameter param )
		{
			if( properties == null )
			{
				throw new ArgumentNullException( "properties", "�p�����[�^��null�ɂ͂ł��܂���B" );
			}

			if( this.displayAreaActivation == true )
			{
				this.logger.warn(
					"���łɃt���[���͐�������Ă��܂��B" );
				return this.frame;
			}

			this.properties = properties;

			this.frame = new GameFrame( this.properties.getFrameSize() );
			AnimationGenerator.getInstance().start(
				this.properties.getHz(),
				this.properties.getFrameSize(),
				this.frame
				);

			SceneSwitcher.swtichScene( 0, param );

			return this.frame;
		}


		/// <summary>
		/// �Q�[���ݒ���擾���܂��B
		/// </summary>
		/// <returns>�Q�[���ݒ�</returns>
		public GameProperties getProperties()
		{
			return this.properties;
		}


		/// <summary>
		/// DisplayArea�̕\����Ԃ��X�V���܂��B
		/// </summary>
		/// <param name="activation">�\�����</param>
		internal void setDisplayAreaActivation(Boolean activation)
		{
			this.displayAreaActivation = activation;
		}
    }
}
