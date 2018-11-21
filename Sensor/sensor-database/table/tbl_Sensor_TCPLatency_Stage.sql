IF (OBJECT_ID(N'[dbo].[tbl_Sensor_TCPLatency_Stage]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_Sensor_TCPLatency_Stage]
  ([dt_session] [smalldatetime] NULL
	,[nvc_source] [nvarchar](50) NULL
	,[nvc_dns] [nvarchar](100) NULL
	,[nvc_ip] [nvarchar](20) NULL
	,[i_latency] [int] NULL)
END