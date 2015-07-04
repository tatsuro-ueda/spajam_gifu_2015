Drop Table Tweet 
Drop Table HVCLog 
Drop Table SpotMaster;

-- Project Name : SpajamMadoben
-- Date/Time    : 2015/07/04 20:28:14
-- Author       : �����@�F��
-- RDBMS Type   : Microsoft SQL Server 2008 �`
-- Application  : A5:SQL Mk-2

-- �c�C�[�g�f�[�^
create table Tweet (
  TweetID VARCHAR(36) not null
  , SpotID VARCHAR(36) not null
  , TweetText VARCHAR(max) not null
  , TweetURL VARCHAR(max) not null
  , CreateDateTime DATETIME not null
  , constraint Tweet_PKC primary key (TweetID)
) ;

-- HVC���O�f�[�^
create table HVCLog (
  LogID VARCHAR(36) not null
  , SpotID VARCHAR(36) not null
  , TweetID VARCHAR(36)
  , Language VARCHAR(2) not null
  , Expression INTEGER not null
  , Age INTEGER not null
  , Sex VARCHAR(1) not null
  , CreateDateTime DATETIME not null
  , constraint HVCLog_PKC primary key (LogID)
) ;

-- �X�|�b�g�}�X�^�[
create table SpotMaster (
  SpotID VARCHAR(36) not null
  , SpotName VARCHAR(max) not null
  , SpotDescription VARCHAR(max)
  , SpotDescriptionAudio VARCHAR(max)
  , SpotImageURL VARCHAR(max)
  , MajorID INTEGER not null
  , MinorID INTEGER not null
  , SortID BIGINT
  , PrefectureID INTEGER
  , StateCityLineID INTEGER
  , Latitude DECIMAL(9,6) not null
  , Longitude DECIMAL(9,6) not null
  , CreateDateTime DATETIME not null
  , constraint SpotMaster_PKC primary key (SpotID)
) ;

EXECUTE sp_addextendedproperty N'MS_Description', N'�c�C�[�g�f�[�^	 �X�|�b�g�ɐ������񂾂Ԃ₫�̃f�[�^', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'�c�C�[�gID	 Tweet����ӂɎ��ʂ���ID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�gID	 �ǂ̃X�|�b�g��Tweet���ꂽ�������ʂ���ID', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�c�C�[�g���e	 Tweet�̉�����͌���', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetText';
EXECUTE sp_addextendedproperty N'MS_Description', N'�c�C�[�g�t�@�C��URL	 Tweet�̉��������t�@�C��URL', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'�쐬����	 Tweet�̍쐬����', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'HVC���O�f�[�^	 �q���[�}���r�W�����R���{�̃��O��o�^����', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'���OID	 HVC�̃��O����ӂɎ��ʂ���ID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'LogID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�gID	 �X�|�b�g(iBeacon)����ӂɎ��ʂ���ID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�c�C�[�gID	 Tweet�������ݎ��Ƀ��O�����т���', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'TweetID';
EXECUTE sp_addextendedproperty N'MS_Description', N'����	 jp:���{�� en:�p�� cn:������', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Language';
EXECUTE sp_addextendedproperty N'MS_Description', N'�\��	 0:���\�� 1:��� 2:���� 3:�{�� 4:������', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Expression';
EXECUTE sp_addextendedproperty N'MS_Description', N'�N��	 �N��0-75', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Age';
EXECUTE sp_addextendedproperty N'MS_Description', N'����	 m:�j�� f:����', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Sex';
EXECUTE sp_addextendedproperty N'MS_Description', N'�쐬����	 HVC���O�̍쐬����', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�}�X�^�[	 �X�|�b�g(iBeacon�̐ݒu�ꏊ)��o�^����}�X�^', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�gID	 �X�|�b�g(iBeacon)����ӂɎ��ʂ���ID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g��	 �X�|�b�g��', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotName';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g����	 �X�|�b�g�̐���', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescription';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g��������URL	 �X�|�b�g�̐���������URL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescriptionAudio';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�摜URL	 �X�|�b�g�摜��URL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotImageURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'���W���[ID	 iBeacon�̃��W���[ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MajorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�}�C�i�[ID	 iBeacon�̃}�C�i�[ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MinorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'���я�	 ���я�', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�s���{��ID	 �s���{������ӂɎ��ʂ���ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'PrefectureID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�s�撬��ID	 �s�撬������ӂɎ��ʂ���ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'StateCityLineID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�ܓx	 iBeacon�ݒu�ꏊ�̈ܓx', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Latitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�x	 iBeacon�̐ݒu�ꏊ�̌o�x', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Longitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�^����	 �X�|�b�g��o�^��������', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'CreateDateTime';
