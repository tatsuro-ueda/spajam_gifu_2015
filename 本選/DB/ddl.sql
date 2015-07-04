Drop Table DeviceToken;
Drop Table AudioCommentary;
Drop Table SpotMaster;
Drop Table Talk;
Drop Table Voice;

-- Project Name : SpajamMadoben
-- Date/Time    : 2015/06/20 10:31:22
-- Author       : �����@�F��
-- RDBMS Type   : Microsoft SQL Server 2008 �`
-- Application  : A5:SQL Mk-2

-- �f�o�C�X�g�[�N���e�[�u��
create table DeviceToken (
  DeviceTokenID VARCHAR(36) not null
  , DeviceToken VARBINARY(max) not null
  , CreateDateTime DATETIME not null
  , constraint DeviceToken_PKC primary key (DeviceTokenID)
) ;

-- ��������f�[�^
create table AudioCommentary (
  AudioCommentaryKey VARCHAR(36) not null
  , AudioCommentaryTitle VARCHAR(200) not null
  , SpotKey VARCHAR(36) not null
  , SortID BIGINT
  , FileID VARCHAR(36)
  , AudioCommentaryResultOriginal VARCHAR(max)
  , AudioCommentaryResultConversion VARCHAR(max)
  , SpeechSynthesisFileID VARCHAR(36)
  , RegisteredUserID VARCHAR(36) not null
  , RegisteredDateTime DATETIME not null
  , constraint AudioCommentary_PKC primary key (AudioCommentaryKey)
) ;

-- �X�|�b�g�}�X�^�[
create table SpotMaster (
  SpotKey VARCHAR(36) not null
  , SpotName VARCHAR(max) not null
  , SpotDescription VARCHAR(max)
  , SpotImageURL VARCHAR(max)
  , MajorID INTEGER not null
  , MinorID INTEGER not null
  , SortID BIGINT
  , PrefectureID INTEGER
  , StateCityLineID INTEGER
  , Latitude DECIMAL(9,6) not null
  , Longitude DECIMAL(9,6) not null
  , RegisteredDateTime DATETIME not null
  , constraint SpotMaster_PKC primary key (SpotKey)
) ;

-- �����e�[�u��
create table Voice (
  TalkID VARCHAR(36) not null
  , IndexID BIGINT not null
  , VoiceID VARCHAR(36)
  , FileID VARCHAR(36)
  , VoiceAnalysisResult VARCHAR(max)
  , TextMiningResult BIGINT
  , TextMiningResultDetail VARCHAR(max)
  , CreateDateTime DATETIME not null
  , constraint Voice_PKC primary key (TalkID,IndexID)
) ;

-- �g�[�N�e�[�u��
create table Talk (
  UserID VARCHAR(36) not null
  , TalkID VARCHAR(36) not null
  , TalkTitle VARCHAR(256) not null
  , SortID BIGINT not null
  , Evaluation VARCHAR(max)
  , EvaluationDetail BIGINT
  , constraint Talk_PKC primary key (UserID,TalkID)
) ;

EXECUTE sp_addextendedproperty N'MS_Description', N'�f�o�C�X�g�[�N���e�[�u��	 Push�ʒm�Ώے[���̃f�o�C�X�g�[�N������ۑ�����', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'�f�o�C�X�g�[�N��ID	 �f�o�C�X�g�[�N������ӂɎ��ʂ���L�[(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'DeviceTokenID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�f�o�C�X�g�[�N��	 �f�o�C�X�g�[�N��', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'DeviceToken';
EXECUTE sp_addextendedproperty N'MS_Description', N'�쐬����', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'��������f�[�^	 �Ώۂ�iBeacon�ɓo�^���ꂽ��������̃f�[�^', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'��������L�[	 ��������f�[�^�̃L�[(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'��������^�C�g��	 ��������̃^�C�g��', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryTitle';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�L�[	 �X�|�b�g�}�X�^�[�̃L�[(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SpotKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'���я�	 ���я�', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'����������t�@�C��ID	 ���[�U�[���瑗��ꂽ��������t�@�C����ID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'FileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'���������͌��ʃe�L�X�g(����)	 �^�����������̉�͌��ʃe�L�X�g�̌���', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryResultOriginal';
EXECUTE sp_addextendedproperty N'MS_Description', N'���������͌��ʃe�L�X�g(�ϊ���)	 �^�����������̉�͌��ʃe�L�X�g�̌����������ϊ���������', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryResultConversion';
EXECUTE sp_addextendedproperty N'MS_Description', N'���������t�@�C��ID	 �^�����������̉�͌��ʃe�L�X�g���������������t�@�C��ID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SpeechSynthesisFileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�^��ID	 �o�^�҂�ID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'RegisteredUserID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�^����	 �f�[�^�̓o�^����', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'RegisteredDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�}�X�^�[	 �X�|�b�g(iBeacon�̐ݒu�ꏊ)��o�^����}�X�^', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�L�[	 �X�|�b�g�}�X�^�[�̃L�[(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g��	 �X�|�b�g��', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotName';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g����	 �X�|�b�g�̐���', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescription';
EXECUTE sp_addextendedproperty N'MS_Description', N'�X�|�b�g�摜URL	 �X�|�b�g�摜��URL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotImageURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'���W���[ID	 iBeacon�̃��W���[ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MajorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�}�C�i�[ID	 iBeacon�̃}�C�i�[ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MinorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'���я�	 ���я�', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�s���{��ID	 �s���{������ӂɎ��ʂ���ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'PrefectureID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�s�撬��ID	 �s�撬������ӂɎ��ʂ���ID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'StateCityLineID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�ܓx	 iBeacon�ݒu�ꏊ�̈ܓx', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Latitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�x	 iBeacon�̐ݒu�ꏊ�̌o�x', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Longitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'�o�^����	 �f�[�^��o�^��������', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'RegisteredDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'�����e�[�u��	 �����t�@�C����ۑ�����e�[�u��', N'SCHEMA', N'dbo', N'TABLE', N'Voice', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'�g�[�NID	 ��b����ӂɎ��ʂ���GUID', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TalkID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�C���f�b�N�XID	 �A��', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'IndexID';
EXECUTE sp_addextendedproperty N'MS_Description', N'����ID	 ��������ӂɎ��ʂ���GUID', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'VoiceID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�t�@�C��ID	 AzureStrage�ɕۑ������t�@�C����(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'FileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'������͌���	 �������API�̌���json������', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'VoiceAnalysisResult';
EXECUTE sp_addextendedproperty N'MS_Description', N'�e�L�X�g�}�C�j���O����	 �e�L�X�g�}�C�j���O�̌��ʃX�e�[�^�X', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TextMiningResult';
EXECUTE sp_addextendedproperty N'MS_Description', N'�e�L�X�g�}�C�j���O���ʏڍ�	 �e�L�X�g�}�C�j���O�̌��ʏڍ�', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TextMiningResultDetail';
EXECUTE sp_addextendedproperty N'MS_Description', N'�쐬����	 �쐬����', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'�g�[�N�e�[�u��	 �����t�@�C���e�[�u����1�̉�b�Ƃ��ăO���[�s���O����e�[�u��', N'SCHEMA', N'dbo', N'TABLE', N'Talk', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'���[�U�[ID	 ���[�U�[����ӂɎ��ʂ���GUID', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'UserID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�g�[�NID	 ��b����ӂɎ��ʂ���GUID', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'TalkID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�g�[�N��	 ��b�̃^�C�g��', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'TalkTitle';
EXECUTE sp_addextendedproperty N'MS_Description', N'���я�	 ���я�', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'�]��	 ��b�̑����]���X�e�[�^�X', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'Evaluation';
EXECUTE sp_addextendedproperty N'MS_Description', N'�]���ڍ�	 ��b�̑����]���ڍ�', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'EvaluationDetail';
