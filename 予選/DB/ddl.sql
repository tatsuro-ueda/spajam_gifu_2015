Drop Table DeviceToken;
Drop Table AudioCommentary;
Drop Table SpotMaster;
Drop Table Talk;
Drop Table Voice;

-- Project Name : SpajamMadoben
-- Date/Time    : 2015/06/20 10:31:22
-- Author       : 平内　孝尚
-- RDBMS Type   : Microsoft SQL Server 2008 ～
-- Application  : A5:SQL Mk-2

-- デバイストークンテーブル
create table DeviceToken (
  DeviceTokenID VARCHAR(36) not null
  , DeviceToken VARBINARY(max) not null
  , CreateDateTime DATETIME not null
  , constraint DeviceToken_PKC primary key (DeviceTokenID)
) ;

-- 音声解説データ
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

-- スポットマスター
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

-- 音声テーブル
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

-- トークテーブル
create table Talk (
  UserID VARCHAR(36) not null
  , TalkID VARCHAR(36) not null
  , TalkTitle VARCHAR(256) not null
  , SortID BIGINT not null
  , Evaluation VARCHAR(max)
  , EvaluationDetail BIGINT
  , constraint Talk_PKC primary key (UserID,TalkID)
) ;

EXECUTE sp_addextendedproperty N'MS_Description', N'デバイストークンテーブル	 Push通知対象端末のデバイストークン情報を保存する', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'デバイストークンID	 デバイストークンを一意に識別するキー(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'DeviceTokenID';
EXECUTE sp_addextendedproperty N'MS_Description', N'デバイストークン	 デバイストークン', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'DeviceToken';
EXECUTE sp_addextendedproperty N'MS_Description', N'作成日時', N'SCHEMA', N'dbo', N'TABLE', N'DeviceToken', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説データ	 対象のiBeaconに登録された音声解説のデータ', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説キー	 音声解説データのキー(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説タイトル	 音声解説のタイトル', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryTitle';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポットキー	 スポットマスターのキー(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SpotKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'並び順	 並び順', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説元ファイルID	 ユーザーから送られた音声解説ファイルのID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'FileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説解析結果テキスト(原文)	 録音した音声の解析結果テキストの原文', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryResultOriginal';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解説解析結果テキスト(変換後)	 録音した音声の解析結果テキストの原文を漢字変換したもの', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'AudioCommentaryResultConversion';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声合成ファイルID	 録音した音声の解析結果テキストを音声合成したファイルID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'SpeechSynthesisFileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'登録者ID	 登録者のID(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'RegisteredUserID';
EXECUTE sp_addextendedproperty N'MS_Description', N'登録日時	 データの登録日時', N'SCHEMA', N'dbo', N'TABLE', N'AudioCommentary', N'COLUMN', N'RegisteredDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'スポットマスター	 スポット(iBeaconの設置場所)を登録するマスタ', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'スポットキー	 スポットマスターのキー(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotKey';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット名	 スポット名', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotName';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット説明	 スポットの説明', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescription';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット画像URL	 スポット画像のURL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotImageURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'メジャーID	 iBeaconのメジャーID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MajorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'マイナーID	 iBeaconのマイナーID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MinorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'並び順	 並び順', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県ID	 都道府県を一意に識別するID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'PrefectureID';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村ID	 市区町村を一意に識別するID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'StateCityLineID';
EXECUTE sp_addextendedproperty N'MS_Description', N'緯度	 iBeacon設置場所の緯度', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Latitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'経度	 iBeaconの設置場所の経度', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Longitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'登録日時	 データを登録した日時', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'RegisteredDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'音声テーブル	 音声ファイルを保存するテーブル', N'SCHEMA', N'dbo', N'TABLE', N'Voice', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'トークID	 会話を一意に識別するGUID', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TalkID';
EXECUTE sp_addextendedproperty N'MS_Description', N'インデックスID	 連番', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'IndexID';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声ID	 音声を一意に識別するGUID', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'VoiceID';
EXECUTE sp_addextendedproperty N'MS_Description', N'ファイルID	 AzureStrageに保存したファイル名(GUID)', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'FileID';
EXECUTE sp_addextendedproperty N'MS_Description', N'音声解析結果	 音声解析APIの結果json文字列', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'VoiceAnalysisResult';
EXECUTE sp_addextendedproperty N'MS_Description', N'テキストマイニング結果	 テキストマイニングの結果ステータス', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TextMiningResult';
EXECUTE sp_addextendedproperty N'MS_Description', N'テキストマイニング結果詳細	 テキストマイニングの結果詳細', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'TextMiningResultDetail';
EXECUTE sp_addextendedproperty N'MS_Description', N'作成日時	 作成日時', N'SCHEMA', N'dbo', N'TABLE', N'Voice', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'トークテーブル	 音声ファイルテーブルを1つの会話としてグルーピングするテーブル', N'SCHEMA', N'dbo', N'TABLE', N'Talk', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ユーザーID	 ユーザーを一意に識別するGUID', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'UserID';
EXECUTE sp_addextendedproperty N'MS_Description', N'トークID	 会話を一意に識別するGUID', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'TalkID';
EXECUTE sp_addextendedproperty N'MS_Description', N'トーク名	 会話のタイトル', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'TalkTitle';
EXECUTE sp_addextendedproperty N'MS_Description', N'並び順	 並び順', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'評価	 会話の総合評価ステータス', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'Evaluation';
EXECUTE sp_addextendedproperty N'MS_Description', N'評価詳細	 会話の総合評価詳細', N'SCHEMA', N'dbo', N'TABLE', N'Talk', N'COLUMN', N'EvaluationDetail';
