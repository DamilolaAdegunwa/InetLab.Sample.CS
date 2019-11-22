Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports Inetlab.SMPP.Logging

Namespace SmppClientDemo

	Public Class TextBoxLogFactory
		Implements ILogFactory

		Private ReadOnly _textBox As TextBox
		Private ReadOnly _minLevel As LogLevel
		Private ReadOnly _logStore As New DataStore(Of String)()

		Public Sub New(ByVal textBox As TextBox, ByVal minLevel As LogLevel)
			_textBox = textBox
			AddHandler _textBox.HandleCreated, Function(sender, args) _textBox.BeginInvoke(New Action(AddressOf AddToTextBox))
			_minLevel = minLevel
		End Sub

		Public Function GetLogger(ByVal loggerName As String) As ILog Implements ILogFactory.GetLogger
			Return New TextBoxLogger(loggerName, _minLevel, AddressOf AddToLog)
		End Function

		Private _isThrottling As Integer = 0

		Private Sub AddToLog(ByVal text As String)
			_logStore.Append(text)

			If _textBox.IsHandleCreated AndAlso Interlocked.CompareExchange(_isThrottling, 1, 0) = 0 Then
				_textBox.BeginInvoke(New Action(AddressOf AddToTextBox))
			End If
		End Sub

		Private Sub AddToTextBox()
			Do While _logStore.HasData
				Dim sb As New StringBuilder()
				For Each line As String In _logStore.TakeWork()
					sb.AppendLine(line)
				Next line

				_textBox.AppendText(sb.ToString())
			Loop

			Interlocked.Exchange(_isThrottling, 0)
		End Sub


		Private Class DataStore(Of T)
			Private ReadOnly _data As New List(Of T)()

			Public Sub Append(ByVal data As T)
				SyncLock _data
					_data.Add(data)
				End SyncLock
			End Sub

			Public ReadOnly Property HasData() As Boolean
				Get
					SyncLock _data
						Return _data.Count > 0
					End SyncLock
				End Get
			End Property

			Public Function TakeWork() As T()
				Dim result() As T
				SyncLock _data
					result = _data.ToArray()
					_data.Clear()
				End SyncLock

				Return result
			End Function
		End Class


		Private Class TextBoxLogger
			Implements ILog

			Private ReadOnly _loggerName As String
			Private ReadOnly _minLevel As LogLevel
			Private ReadOnly _append As Action(Of String)


			Public Sub New(ByVal loggerName As String, ByVal minLevel As LogLevel, ByVal append As Action(Of String))
				_loggerName = loggerName
				_minLevel = minLevel
				_append = append
			End Sub


			Public Function IsEnabled(ByVal level As LogLevel) As Boolean Implements ILog.IsEnabled
				Return level >= _minLevel
			End Function

			Public Sub Write(ByVal level As LogLevel, ByVal message As String, ByVal ex As Exception, ParamArray ByVal args() As Object) Implements ILog.Write
				If Not IsEnabled(level) Then
					Return
				End If

				AddToLog("{0} ({1}) {2}{3}", level, _loggerName, String.Format(message, args),If(ex IsNot Nothing, ", Exception: " & ex.ToString(), ""))

			End Sub

			Private Sub AddToLog(ByVal message As String, ParamArray ByVal args() As Object)
				_append(String.Format("{0:HH:mm:ss.fff}: {1}", Date.Now, String.Format(message, args)))

			End Sub

		End Class

	End Class
End Namespace
