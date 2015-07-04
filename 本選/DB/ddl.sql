Drop Table Tweet 
Drop Table HVCLog 
Drop Table SpotMaster;

-- Project Name : SpajamMadoben
-- Date/Time    : 2015/07/04 20:28:14
-- Author       : 平内　孝尚
-- RDBMS Type   : Microsoft SQL Server 2008 ～
-- Application  : A5:SQL Mk-2

-- ツイートデータ
create table Tweet (
  TweetID VARCHAR(36) not null
  , SpotID VARCHAR(36) not null
  , TweetText VARCHAR(max) not null
  , TweetURL VARCHAR(max) not null
  , CreateDateTime DATETIME not null
  , constraint Tweet_PKC primary key (TweetID)
) ;

-- HVCログデータ
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

-- スポットマスター
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

EXECUTE sp_addextendedproperty N'MS_Description', N'ツイートデータ	 スポットに吹き込んだつぶやきのデータ', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ツイートID	 Tweetを一意に識別するID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetID';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポットID	 どのスポットでTweetされたかを識別するID', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'ツイート内容	 Tweetの音声解析結果', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetText';
EXECUTE sp_addextendedproperty N'MS_Description', N'ツイートファイルURL	 Tweetの音声合成ファイルURL', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'TweetURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'作成日時	 Tweetの作成日時', N'SCHEMA', N'dbo', N'TABLE', N'Tweet', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'HVCログデータ	 ヒューマンビジョンコンボのログを登録する', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'ログID	 HVCのログを一意に識別するID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'LogID';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポットID	 スポット(iBeacon)を一意に識別するID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'ツイートID	 Tweet吹き込み時にログを結びつける', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'TweetID';
EXECUTE sp_addextendedproperty N'MS_Description', N'言語	 jp:日本語 en:英語 cn:中国語', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Language';
EXECUTE sp_addextendedproperty N'MS_Description', N'表情	 0:無表情 1:喜び 2:驚き 3:怒り 4:哀しみ', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Expression';
EXECUTE sp_addextendedproperty N'MS_Description', N'年齢	 年齢0-75', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Age';
EXECUTE sp_addextendedproperty N'MS_Description', N'性別	 m:男性 f:女性', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'Sex';
EXECUTE sp_addextendedproperty N'MS_Description', N'作成日時	 HVCログの作成日時', N'SCHEMA', N'dbo', N'TABLE', N'HVCLog', N'COLUMN', N'CreateDateTime';

EXECUTE sp_addextendedproperty N'MS_Description', N'スポットマスター	 スポット(iBeaconの設置場所)を登録するマスタ', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', N'スポットID	 スポット(iBeacon)を一意に識別するID(UUID)', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotID';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット名	 スポット名', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotName';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット説明	 スポットの説明', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescription';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット説明音声URL	 スポットの説明音声のURL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotDescriptionAudio';
EXECUTE sp_addextendedproperty N'MS_Description', N'スポット画像URL	 スポット画像のURL', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SpotImageURL';
EXECUTE sp_addextendedproperty N'MS_Description', N'メジャーID	 iBeaconのメジャーID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MajorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'マイナーID	 iBeaconのマイナーID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'MinorID';
EXECUTE sp_addextendedproperty N'MS_Description', N'並び順	 並び順', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'SortID';
EXECUTE sp_addextendedproperty N'MS_Description', N'都道府県ID	 都道府県を一意に識別するID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'PrefectureID';
EXECUTE sp_addextendedproperty N'MS_Description', N'市区町村ID	 市区町村を一意に識別するID', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'StateCityLineID';
EXECUTE sp_addextendedproperty N'MS_Description', N'緯度	 iBeacon設置場所の緯度', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Latitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'経度	 iBeaconの設置場所の経度', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'Longitude';
EXECUTE sp_addextendedproperty N'MS_Description', N'登録日時	 スポットを登録した日時', N'SCHEMA', N'dbo', N'TABLE', N'SpotMaster', N'COLUMN', N'CreateDateTime';
