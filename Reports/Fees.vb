












Partial Class Fees
    

    Partial Class Vw_SRFYDataTable

        Private Sub Vw_SRFYDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.VoucherNoColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

Namespace FeesTableAdapters
    
    Partial Public Class Vw_StudentDetainDetailsTableAdapter
    End Class
End Namespace
