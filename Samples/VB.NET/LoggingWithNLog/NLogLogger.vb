Imports System
Imports Inetlab.SMPP.Logging
Imports NLog

Public Class NLogLoggerFactory
	Implements ILogFactory

	Public Function GetLogger(ByVal loggerName As String) As ILog Implements ILogFactory.GetLogger
		Return New NLogLogger(loggerName)
	End Function
End Class

Public Class NLogLogger
	Implements ILog

	Private ReadOnly _internalLog As Logger

	Public Sub New(ByVal loggerName As String)
		_internalLog = NLog.LogManager.GetLogger(loggerName)
	End Sub

	Public Function IsEnabled(ByVal level As Inetlab.SMPP.Logging.LogLevel) As Boolean Implements ILog.IsEnabled
		Return _internalLog.IsEnabled(GetLevel(level))
	End Function



	Public Sub Write(ByVal level As Inetlab.SMPP.Logging.LogLevel, ByVal message As String, ByVal ex As Exception, ParamArray ByVal args() As Object) Implements ILog.Write
		_internalLog.Log(GetLevel(level), ex, message, args)
	End Sub

	Private Function GetLevel(ByVal level As Inetlab.SMPP.Logging.LogLevel) As NLog.LogLevel
		Select Case level
			Case Inetlab.SMPP.Logging.LogLevel.All, Inetlab.SMPP.Logging.LogLevel.Verbose
				Return NLog.LogLevel.Trace
			Case Inetlab.SMPP.Logging.LogLevel.Debug
				Return NLog.LogLevel.Debug
			Case Inetlab.SMPP.Logging.LogLevel.Info
				Return NLog.LogLevel.Info
			Case Inetlab.SMPP.Logging.LogLevel.Warning
				Return NLog.LogLevel.Warn
			Case Inetlab.SMPP.Logging.LogLevel.Error
				Return NLog.LogLevel.Error
			Case Inetlab.SMPP.Logging.LogLevel.Fatal
				Return NLog.LogLevel.Fatal
			Case Inetlab.SMPP.Logging.LogLevel.Off
				Return NLog.LogLevel.Off
			Case Else
				Throw New ArgumentOutOfRangeException(NameOf(level), level, Nothing)
		End Select
	End Function
End Class