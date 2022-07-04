Imports System.Globalization
Imports CrystalDecisions.Shared

Public Class FeesReport
    Dim frm As New FrmReportViewer
    Public text As String = ""


    Public Function YearQuotaCategoryDetailsTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTAC, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function
    Public Sub StudentExternalMarksReport(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal Year As Integer, ByVal Semester As String, ByVal Test As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Fees
        Dim RepComm As String = ""
        If Test = "External Marks" Then
            Dim CryStudentdetails As New rptExternalMarks
            RepComm = "Student External Marks"
            Dim Da As New FeesTableAdapters.Vw_StudentMarksDetailsTableAdapter
            Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByBatch(Ds.Vw_StudentMarksDetails, Semester, Year, Batch, Program, Course, Section)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student External Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            CryStudentdetails.SetParameterValue("UserName", UN)
            CryStudentdetails.SetParameterValue("Batch", Batch)
            CryStudentdetails.SetParameterValue("Program", Program)
            CryStudentdetails.SetParameterValue("Course", Course)
            CryStudentdetails.SetParameterValue("Section", Section)
            CryStudentdetails.SetParameterValue("Year", Year)
            CryStudentdetails.SetParameterValue("Semester", Semester)
            CryStudentdetails.SetParameterValue("Test", Test)
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()


        End If
    End Sub

    Public Sub OpeningBalance(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptOpeningBalance
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Closing Balances"
        Dim Da As New FeesTableAdapters.Vw_OpeningBalanceTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByFinancialYear(Ds.Vw_OpeningBalance, YearCode)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Opeinging Balances"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub FinancialYearRefund(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialYearRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Financial Year Refund Closing Balances"
        Dim Da As New FeesTableAdapters.tbl_fees_FYRefundTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.Fill(Ds.tbl_fees_FYRefund)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Financial Year Refund Closing Balances"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function FinancialYearRefundPdf(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialYearRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Financial Year Refund Closing Balances"
        Dim Da As New FeesTableAdapters.tbl_fees_FYRefundTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.Fill(Ds.tbl_fees_FYRefund)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Financial Year Refund Closing Balances"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub JEConsolidate(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New JEFinancialYearClosing
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Opening Balances"
        Dim Da As New FeesTableAdapters.tbl_fees_JEConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByCurrentFY(Ds.tbl_fees_JEConsolidate, YearCode)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Financial Year Closing & Opening Balances"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function JEConsolidatePdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New JEFinancialYearClosing
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Opening Balances"
        Dim Da As New FeesTableAdapters.tbl_fees_JEConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByCurrentFY(Ds.tbl_fees_JEConsolidate, YearCode)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Financial Year Closing & Opening Balances"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub JEConsolidateSummary(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New JEFinancialYearClosingSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Opening Balances Summary"
        Dim Da As New FeesTableAdapters.tbl_fees_JEConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByCFYSummary(Ds.tbl_fees_JEConsolidate, YearCode)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Financial Year Closing & Opening Balances Summary"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function JEConsolidateSummaryPdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New JEFinancialYearClosingSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Opening Balances Summary"
        Dim Da As New FeesTableAdapters.tbl_fees_JEConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByCFYSummary(Ds.tbl_fees_JEConsolidate, YearCode)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Financial Year Closing & Opening Balances Summary"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub IssueCertificate(ByVal UserType As Integer, ByVal Certificate As String, ByVal UN As String, ByVal CertificateName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCerticateIssue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = CertificateName & " Student Details"
        Dim Da As New FeesTableAdapters.Vw_IssuedCertificatesTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByCertificate(Ds.Vw_IssuedCertificates, Certificate)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = CertificateName & " Student Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("CName", CertificateName)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub NotTCIssueCertificate(ByVal UserType As Integer, ByVal UN As String, ByVal CertificateName As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCerticateNotIssue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = CertificateName & " Student Details"
        Dim Da As New FeesTableAdapters.Vw_TCNotIssuedTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByProgram(Ds.Vw_TCNotIssued, Program)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = CertificateName & " Student Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("CName", CertificateName)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)
        frm.Show()

    End Sub

    Public Sub ODReport(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptOD
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Out Of Data"
        Dim Da As New FeesTableAdapters.Vw_ODTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByBetweenDates(Ds.Vw_OD, strdt, strEndt, "Approved")

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Out Of Data Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function ODReportPdf(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptOD
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Out Of Data"
        Dim Da As New FeesTableAdapters.Vw_ODTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByBetweenDates(Ds.Vw_OD, StDt, EndDt, "Approved")

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Out Of Data Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub SusPenseReport(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSuspense
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Suspense List"
        Dim Da As New FeesTableAdapters.Vw_SuspenseListTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByDate(Ds.Vw_SuspenseList, strdt, strEndt, "Previous Financial Year Suspense")

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Out Of Data Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function SusPenseReportPdf(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSuspense
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Suspense List"
        Dim Da As New FeesTableAdapters.Vw_SuspenseListTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByDate(Ds.Vw_SuspenseList, StDt, EndDt, "Previous Financial Year Suspense")

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Out Of Data Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub Monthconsolidate(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Month Consolidate"
        Dim Da As New FeesTableAdapters.Vw_MonthconsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByFinancialYear(Ds.Vw_Monthconsolidate, strdt, strEndt)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Financial Year Month Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub MonthconsolidateExtraPaid(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthConsolidateExtrapaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " Financial Year Month Consolidate"
        Dim Da As New FeesTableAdapters.Vw_MonthconsolidateExtraPaidTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByFinancialYear(Ds.Vw_MonthconsolidateExtraPaid, strdt, strEndt)

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Financial Year Month Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentPayTransactionReport(ByVal UserType As Integer, ByVal Year As Integer, ByVal StudentId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPayTransactions

        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudentYear(Ds.Vw_StudentPayTransactions, Year, StudentId)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("Student", StudentId)
        'frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub FinancialJV(ByVal UserType As Integer, ByVal YearCode As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptJV
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Journal Entry"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Dim Da As New FeesTableAdapters.tbl_fees_JVTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByDate(Ds.tbl_fees_JV, strdt, strEndt)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Journal Entry"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub ProgramConsolidate(ByVal UserType As Integer, ByVal UN As String, ByVal STDT As Date, ByVal EDT As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptProgramConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = "Program Consolidate Report"
        ' Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Dim Da As New FeesTableAdapters.tbl_fees_ProgramConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_ProgramConsolidate)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Program Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", STDT)
        CryStudentdetails.SetParameterValue("ToDate", EDT)

        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub PaymentProgramConsolidate(ByVal UserType As Integer, ByVal UN As String, ByVal STDT As Date, ByVal EDT As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentProgramConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = "Payment Program Consolidate Report"
        ' Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Dim Da As New FeesTableAdapters.tbl_fees_PaymentProgramConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_PaymentProgramConsolidate)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Payment Program Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", STDT)
        CryStudentdetails.SetParameterValue("ToDate", EDT)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub ExtraPaid(ByVal UserType As Integer, ByVal StudentStatus As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptExtraPaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Extra Paid List"
        Dim Da As New FeesTableAdapters.Vw_PayExtraDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStatus(Ds.Vw_PayExtraDetails, StudentStatus, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Extra Paid List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub
    Public Sub StudentPayDateTransactionReport(ByVal UserType As Integer, ByVal Year As Integer, ByVal Dt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByDate(Ds.Vw_StudentPayTransactions, Year, strdt)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        CryStudentdetails.SetParameterValue("UserName", UN)

        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentPayDateTransactionReportALL(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByAllTransactions(Ds.Vw_StudentPayDateReport, strdt, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        CryStudentdetails.SetParameterValue("UserName", UN)

        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentDateReportALL(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReconcilationApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details - " & Status
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByNDFDate(Ds.Vw_StudentApprovalReport, strdt, Status, PaymentMode, Program)
        Else
            Da.FillByDate(Ds.Vw_StudentApprovalReport, strdt, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoDate(Ds.Vw_StudentApprovalReport, strdt, Status, "%" & PaymentMode & "%")
        'Else
        'End If

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub UserStudentDateReportALL(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserStudentReconcilationApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details - " & Status
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByNDFDate(Ds.Vw_StudentApprovalReport, strdt, Status, PaymentMode, Program)
        Else
            Da.FillByDate(Ds.Vw_StudentApprovalReport, strdt, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoDate(Ds.Vw_StudentApprovalReport, strdt, Status, "%" & PaymentMode & "%")
        'Else
        'End If

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub StudentYearDateReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReconcilationApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, PaymentMode, Program)

        Else
            Da.FillByYearDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, Program)

        End If
        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, "%" & PaymentMode & "%")
        'Else


        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub UserStudentYearDateReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserStudentReconcilationApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, PaymentMode, Program)

        Else
            Da.FillByYearDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, Program)

        End If
        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoDate(Ds.Vw_StudentApprovalReport, strdt, Year, Status, "%" & PaymentMode & "%")
        'Else


        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("Date", strdt)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentRefundReport(ByVal UserType As Integer, ByVal Dt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Refund Details"
        Dim Da As New FeesTableAdapters.Vw_Refund_DetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByDate(Ds.Vw_Refund_Details, strdt)
        DaCollege.Fill(Ds.Vw_College)
        ' DaRefund.FillByDate(Ds.tbl_fees_RefundMaster, strdt)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        'CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))


        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Refund Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentPeriodRefundReport(ByVal UserType As Integer, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Refund Details"
        Dim Da As New FeesTableAdapters.Vw_Refund_DetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByBetweenDates(Ds.Vw_Refund_Details, strdt, strEndt)
        'DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Refund Details"

        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)

        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentPeriodRefundReportPdf(ByVal UserType As Integer, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Refund Details"
        Dim Da As New FeesTableAdapters.Vw_Refund_DetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByBetweenDates(Ds.Vw_Refund_Details, Dt, EndDt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Refund Details"

        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)

        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentPeriodRefundReportConsolidate(ByVal UserType As Integer, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentRefundConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "List of Refund Consolidate Amounts Date wise"
        Dim Da As New FeesTableAdapters.Vw_Refund_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByConsolidateRefundDT(Ds.Vw_Refund_Consolidate, strdt, strEndt)
        'DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List of Refund Consolidate Amounts Date wise"

        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)

        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentPeriodRefundReportConsolidatePdf(ByVal UserType As Integer, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentRefundConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "List of Refund Consolidate Amounts Date wise"
        Dim Da As New FeesTableAdapters.Vw_Refund_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByConsolidateRefundDT(Ds.Vw_Refund_Consolidate, Dt, EndDt)
        'DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List of Refund Consolidate Amounts Date wise"

        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)

        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub MonthReconsilationReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_PaymentModeConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByConsolidate(Ds.Vw_PaymentModeConsolidate, strdt, strEndt, Program)
        Else
            Da.FillByAllConsolidate(Ds.Vw_PaymentModeConsolidate, strdt, strEndt)
        End If

        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function MonthReconsilationReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_PaymentModeConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByConsolidate(Ds.Vw_PaymentModeConsolidate, Dt, EndDt, Program)
        Else
            Da.FillByAllConsolidate(Ds.Vw_PaymentModeConsolidate, Dt, EndDt)
        End If

        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub MonthReconsilationAllReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_ReconsilationConslidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByReconsilation(Ds.Vw_ReconsilationConslidate, Program, strdt, strEndt)
        Else
            Da.FillByReconsilationAll(Ds.Vw_ReconsilationConslidate, strdt, strEndt)
        End If

        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function MonthReconsilationAllReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_ReconsilationConslidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByReconsilation(Ds.Vw_ReconsilationConslidate, Program, Dt, EndDt)
        Else
            Da.FillByReconsilationAll(Ds.Vw_ReconsilationConslidate, Dt, EndDt)
        End If

        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub MonthReconsilationAllReportHOA(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAllHOA
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        Dim Da As New FeesTableAdapters.tbl_fees_HOATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        Da.Fill(Ds.tbl_fees_HOA)
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function MonthReconsilationAllReportHOAPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal Month As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAllHOA
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        Dim Da As New FeesTableAdapters.tbl_fees_HOATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        Da.Fill(Ds.tbl_fees_HOA)
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)
        CryStudentdetails.SetParameterValue("Month", UCase(Month))
        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub MonthReconsilationAllReportHOADt(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAllHOADt
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        Dim Da As New FeesTableAdapters.tbl_fees_HOATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        Da.Fill(Ds.tbl_fees_HOA)
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)

        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function MonthReconsilationAllReportHOADtPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPaymentReconcilationAllHOADt
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        Dim Da As New FeesTableAdapters.tbl_fees_HOATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        Da.Fill(Ds.tbl_fees_HOA)
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT HEAD OF ACCOUNT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)

        CryStudentdetails.SetParameterValue("PName", UCase(PName))
        EnableButtons(UserType)

        'frm.Show()

        Return CryStudentdetails

    End Function


    Public Sub ReconsilationReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthPaymentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_PaymentModeConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByConsolidate(Ds.Vw_PaymentModeConsolidate, strdt, strEndt, Program)
        Else
            Da.FillByAllConsolidate(Ds.Vw_PaymentModeConsolidate, strdt, strEndt)

        End If
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)

        CryStudentdetails.SetParameterValue("PName", PName)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function ReconsilationReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthPaymentReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_PaymentModeConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByConsolidate(Ds.Vw_PaymentModeConsolidate, Dt, EndDt, Program)
        Else
            Da.FillByAllConsolidate(Ds.Vw_PaymentModeConsolidate, Dt, EndDt)

        End If
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)

        CryStudentdetails.SetParameterValue("PName", PName)

        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub ReconsilationAllReport(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthPaymentReconcilationAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_ReconsilationConslidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByReconsilation(Ds.Vw_ReconsilationConslidate, Program, strdt, strEndt)
        Else
            Da.FillByReconsilationAll(Ds.Vw_ReconsilationConslidate, strdt, strEndt)
        End If
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)

        CryStudentdetails.SetParameterValue("PName", PName)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function ReconsilationAllReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthPaymentReconcilationAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "RECONSILATION STATEMENT"
        Dim Da As New FeesTableAdapters.Vw_ReconsilationConslidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        '   Dim DaRefund As New FeesTableAdapters.tbl_fees_RefundMasterTableAdapter


        'Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByReconsilation(Ds.Vw_ReconsilationConslidate, Program, Dt, EndDt)
        Else
            Da.FillByReconsilationAll(Ds.Vw_ReconsilationConslidate, Dt, EndDt)
        End If
        ' DaRefund.FillByBetweenDates(Ds.tbl_fees_RefundMaster, strdt, strEndt)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        ' CryStudentdetails.Subreports.Item("rptStudentRefundDetails.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "MONTHLY RECONSILATION STATEMENT"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", Dt)
        CryStudentdetails.SetParameterValue("EndDate", EndDt)

        CryStudentdetails.SetParameterValue("PName", PName)

        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub TransactionFYReport(ByVal UserType As Integer, ByVal Dt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialYearTransaction
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " FINANCIAL YEAR TRANSACTIONS"
        Dim Da As New FeesTableAdapters.Vw_TransConsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False
        Da.FillByFinancialYear(Ds.Vw_TransCons, strdt, strEndt)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "FINANCIAL YEAR TRANSACTION"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("FromDate", strdt)
        CryStudentdetails.SetParameterValue("EndDate", strEndt)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub StudentReAdmissionList(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal type As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDuesDR
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "List Of Re-Admission Due"
        Dim Da As New FeesTableAdapters.Vw_DRATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If type = "Same Batch ReAdmission" Then
            Da.FillBySameBatch(Ds.Vw_DRA, strdt, strEndt)
        ElseIf type = "ReAdmission" Then
            Da.FillByRA(Ds.Vw_DRA, strdt, strEndt)
        Else
            Da.FillByDates(Ds.Vw_DRA, strdt, strEndt)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Re-Admission Due"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentReAdmissionListPdf(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal type As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDuesDR
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "List Of Re-Admission Due"
        Dim Da As New FeesTableAdapters.Vw_DRATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If type = "Same Batch ReAdmission" Then
            Da.FillBySameBatch(Ds.Vw_DRA, StDt, EndDt)
        ElseIf type = "ReAdmission" Then
            Da.FillByRA(Ds.Vw_DRA, StDt, EndDt)
        Else
            Da.FillByDates(Ds.Vw_DRA, StDt, EndDt)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Re-Admission Due"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        Return CryStudentdetails
        ' frm.Show()

    End Function


    Public Sub StudentPayDatesTransactionReport(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        Da.FillByBetweenDates(Ds.Vw_StudentPayDateReport, Year, strdt, strEndt, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function StudentPayDatesTransactionReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        Da.FillByBetweenDates(Ds.Vw_StudentPayDateReport, Year, StDt, EndDt, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentDiscontinue(ByVal UserType As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentDiscontinue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Discontinue Details"
        Dim Da As New FeesTableAdapters.Vw_DiscontinueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)


        Da.Fill(Ds.Vw_Discontinue)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Discontinue"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub StudentDiscontinueFY(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentDiscontinue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Discontinue List "
        Dim Da As New FeesTableAdapters.Vw_DiscontinueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByFinancialYear(Ds.Vw_Discontinue, strdt, strEndt)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Discontinue"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("STDT", strdt)
        CryStudentdetails.SetParameterValue("EDT", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)

        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentDiscontinueFYPdf(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentDiscontinue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Discontinue List "
        Dim Da As New FeesTableAdapters.Vw_DiscontinueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Da.FillByFinancialYear(Ds.Vw_Discontinue, StDt, EndDt)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Discontinue"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("STDT", StDt)
        CryStudentdetails.SetParameterValue("EDT", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)

        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentPayDatesTransactionReportALL(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByAllTransactionsBetweenDates(Ds.Vw_StudentPayDateReport, strdt, strEndt, Program)
        Else
            Da.FillByAllTransactionsBetweenDatesAllPrograms(Ds.Vw_StudentPayDateReport, strdt, strEndt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentPayDatesTransactionReportALLPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByAllTransactionsBetweenDates(Ds.Vw_StudentPayDateReport, StDt, EndDt, Program)
        Else
            Da.FillByAllTransactionsBetweenDatesAllPrograms(Ds.Vw_StudentPayDateReport, StDt, EndDt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentReceivedFinancial(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYear
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancial(Ds.Vw_SRFY, YearCode, Program, strdt, strEndt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFY, YearCode, strdt, strEndt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentReceivedFinancialPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYear
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancial(Ds.Vw_SRFY, YearCode, Program, StDt, EndDt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFY, YearCode, StDt, EndDt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub StudentReceivedFinancialTO(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYearTO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYTTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancialYear(Ds.Vw_SRFYT, YearCode, Program, strdt, strEndt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFYT, YearCode, strdt, strEndt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentReceivedFinancialTOPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYearTO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYTTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancialYear(Ds.Vw_SRFYT, YearCode, Program, StDt, EndDt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFYT, YearCode, StDt, EndDt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub StudentReceivedFinancialE(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYearE
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Advance Fee Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancialYear(Ds.Vw_SRFYE, YearCode, Program, strdt, strEndt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFYE, YearCode, strdt, strEndt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentReceivedFinancialEPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReceivedFinancialYearE
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Advance Fee Details"
        Dim Da As New FeesTableAdapters.Vw_SRFYETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        If Program <> "0" Then
            Da.FillByProgramFinancialYear(Ds.Vw_SRFYE, YearCode, Program, StDt, EndDt)
        Else
            Da.FillByFinancialYear(Ds.Vw_SRFYE, YearCode, StDt, EndDt)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentPayDatesTransactionReportRALL(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal RF As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptRPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        Da.FillByAllRTransactionsBetweenDates(Ds.Vw_StudentPayDateReport, strdt, strEndt, RF, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentPayDatesTransactionReportRALLPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal RF As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptRPeriodReconcilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDateReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Ds.EnforceConstraints = False

        Da.FillByAllRTransactionsBetweenDates(Ds.Vw_StudentPayDateReport, StDt, EndDt, RF, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentApproveReportALL(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "User Wise Student Pay Transaction Details - " & Status
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            If Program <> "0" Then
                Da.FillByNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, PaymentMode, Program)
            Else
                Da.FillByNDFBetweenDatesProgram(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, PaymentMode)

            End If
        Else
            If Program <> "0" Then
                Da.FillByBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, Program)
            Else

                Da.FillByDatesBetweenAll(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status)
            End If

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function StudentApproveReportALLPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "User Wise Student Pay Transaction Details - " & Status
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            If Program <> "0" Then
                Da.FillByNDFBetweenDates(Ds.Vw_StudentApprovalReport, StDt, EndDt, Status, PaymentMode, Program)
            Else
                Da.FillByNDFBetweenDatesProgram(Ds.Vw_StudentApprovalReport, StDt, EndDt, Status, PaymentMode)

            End If
        Else
            If Program <> "0" Then
                Da.FillByBetweenDates(Ds.Vw_StudentApprovalReport, StDt, EndDt, Status, Program)
            Else

                Da.FillByDatesBetweenAll(Ds.Vw_StudentApprovalReport, StDt, EndDt, Status)
            End If

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", StDt)
        CryStudentdetails.SetParameterValue("ToDate", EndDt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function

    Public Function UserStudentApproveReportALLPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "User Wise Student Pay Transaction Details - " & Status
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            If Program <> "0" Then
                Da.FillByNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, PaymentMode, Program)
            Else
                Da.FillByNDFBetweenDatesProgram(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, PaymentMode)

            End If
        Else
            If Program <> "0" Then
                Da.FillByBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, Program)
            Else

                Da.FillByDatesBetweenAll(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status)
            End If

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub StudentApproveReportYear(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, PaymentMode, Program)
        Else
            Da.FillByYearBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function StudentApproveReportYearPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, PaymentMode, Program)
        Else
            Da.FillByYearBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub SpecialTC(ByVal UserType As Integer, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Special TC Approved By Special User"
        Dim Da As New FeesTableAdapters.Vw_SpecialTCTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Da.FillByDate(Ds.Vw_SpecialTC, strdt, strEndt)


        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Special TC Approved"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub UserStudentApproveReportYear(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = " User Wise Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, PaymentMode, Program)
        Else
            Da.FillByYearBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function UserStudentApproveReportYearPdf(ByVal UserType As Integer, ByVal Program As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Year As Integer, ByVal Status As String, ByVal PaymentMode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptUserPeriodApproval
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = " User Wise Student Pay Transaction Details"
        Dim Da As New FeesTableAdapters.Vw_StudentApprovalReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        If PaymentMode <> "0" Then
            Da.FillByYearNDFBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, PaymentMode, Program)
        Else
            Da.FillByYearBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, Program)

        End If

        'If PaymentMode = "Challan" Then
        'ElseIf PaymentMode = "DD" OrElse PaymentMode = "FT" Then
        '    Da.FillByYearCHNoBetweenDates(Ds.Vw_StudentApprovalReport, strdt, strEndt, Year, Status, "%" & PaymentMode & "%")
        'Else
        'End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Pay Transactions"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("FrDate", strdt)
        CryStudentdetails.SetParameterValue("ToDate", strEndt)
        CryStudentdetails.SetParameterValue("UserName", UN)
        frm.oReport = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentYearPayableReport(ByVal UserType As Integer, ByVal Year As Integer)
        'frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)

        RepComm = text & "Student Payable Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByPaid(Ds.Vw_StudentPayDetails, "Pending", Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Payable Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub StudentYearDueReport(ByVal UserType As Integer, ByVal Year As Integer)
        'frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentDues
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)

        RepComm = text & "Student Payable Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByPaid(Ds.Vw_StudentPayDetails, "Pending", Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Payable Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentYearFeeReport(ByVal UserType As Integer, ByVal Year As Integer)
        'frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Student Fee Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Payable Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub ModifyChallan(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptModified
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Year & " Year Challan Modified Details"
        Dim Da As New FeesTableAdapters.Vw_ModifiedDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaModified As New FeesTableAdapters.Vw_ModifiedListTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_ModifiedDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        DaModified.FillByYear(Ds.Vw_ModifiedList, Year)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.Subreports.Item("rptModifiedList.rpt").SetDataSource(Ds.Tables.Item("Ds.Vw_RefundDetails"))

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Modified Challan Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentYearPaidReport(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Student Fee Received Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByPaid(Ds.Vw_StudentPayDetails, "Paid", Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Paid Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentYearFeePaid(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentFeePaid
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Student Received Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Fee Paid Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentYearFeePayable(ByVal UserType As Integer, ByVal Year As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentFeePayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Student Payable Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Fee Payable Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentYearFeeTotal(ByVal UserType As Integer, ByVal Year As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentTotal
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Student Total Fee Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Actual Fee Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub NextStudentYearFeeTotal(ByVal UserType As Integer, ByVal Year As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptNextYearStudentTotal
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = Year + 1 & " Year Student Total Fee Details"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Actual Fee Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub NextStudentYearFeeActual(ByVal UserType As Integer, ByVal FY As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptNextYearActual
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = " Next Financial Year Budget Report"
        Dim Da As New FeesTableAdapters.tbl_NextYearTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_NextYear, FY)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Next Financial Year Budget Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function NextStudentYearFeeActualPdf(ByVal UserType As Integer, ByVal FY As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptNextYearActual
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = " Next Financial Year Budget Report"
        Dim Da As New FeesTableAdapters.tbl_NextYearTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_NextYear, FY)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = " Next Financial Year Budget Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub AcademicReport(ByVal UserType As Integer, ByVal FY As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAcademicReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = FY & " Academic Report"
        Dim Da As New FeesTableAdapters.tbl_AcademicYearTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_AcademicYear, FY)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = FY & "Academic Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function AcademicReportPdf(ByVal UserType As Integer, ByVal FY As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAcademicReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = FY & " Academic Report"
        Dim Da As New FeesTableAdapters.tbl_AcademicYearTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_AcademicYear, FY)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = FY & "Academic Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub ALLFYDUE(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptALLFYDUE
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "All Financial Year Due Consolidate"
        Dim Da As New FeesTableAdapters.Vw_AllFYDUETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_AllFYDUE)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "All Financial Year Due Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function ALLFYDUEPdf(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptALLFYDUE
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "All Financial Year Due Consolidate"
        Dim Da As New FeesTableAdapters.Vw_AllFYDUETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_AllFYDUE)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "All Financial Year Due Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails


        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub BudgetReport(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBudget
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
        Else
            RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Financial Year  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Financial Year Closing  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BudgetReportPdf(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBudget
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
        Else
            RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Financial Year  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Financial Year Closing  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        Return CryStudentdetails
        ' frm.Show()

    End Function



    Public Sub IntakeQuotaSummaryReport(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New IntakeSummaryReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Intake Quota Summary  " & YearCode & " as on " & Dt
        Else
            RepComm = "Intake Quota Summary  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Intake Quota Summary  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Intake Quota Summary  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub




    Public Function IntakeQuotaSummaryReportPdf(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New IntakeSummaryReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Intake Quota Summary  " & YearCode & " as on " & Dt
        Else
            RepComm = "Intake Quota Summary  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Intake Quota Summary  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Intake Quota Summary  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub DUEPROGRAMS(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New DuePrograms
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        Else
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.Vw_DuePrpgramTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        Dim DaBE As New FeesTableAdapters.Vw_DuePrpgramBETableAdapter
        Dim DaME As New FeesTableAdapters.Vw_DuePrpgramMETableAdapter
        Dim DaProgramTotal As New FeesTableAdapters.Vw_DuePrpgramTotalTableAdapter



        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.Vw_DuePrpgram, YearCode, "Active")
        DaCollege.Fill(Ds.Vw_College)


        DaBE.FillByFYBE(Ds.Vw_DuePrpgramBE, YearCode, "Active", "B.E")
        DaME.FillByFYME(Ds.Vw_DuePrpgramME, YearCode, "Active", "M.E")

        DaProgramTotal.FillByFY(Ds.Vw_DuePrpgramTotal, YearCode, "Active")

        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        'CryStudentdetails.SetParameterValue("PName", Program)
        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function DUEPROGRAMSPdf(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New DuePrograms
        Dim Ds As New Fees
        Dim RepComm As String = ""

        If FY = "Current Financial Year" Then
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        Else
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.Vw_DuePrpgramTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        Dim DaBE As New FeesTableAdapters.Vw_DuePrpgramBETableAdapter
        Dim DaME As New FeesTableAdapters.Vw_DuePrpgramMETableAdapter
        Dim DaProgramTotal As New FeesTableAdapters.Vw_DuePrpgramTotalTableAdapter



        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.Vw_DuePrpgram, YearCode, "Active")
        DaCollege.Fill(Ds.Vw_College)


        DaBE.FillByFYBE(Ds.Vw_DuePrpgramBE, YearCode, "Active", "B.E")
        DaME.FillByFYME(Ds.Vw_DuePrpgramME, YearCode, "Active", "M.E")

        DaProgramTotal.FillByFY(Ds.Vw_DuePrpgramTotal, YearCode, "Active")

        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        'CryStudentdetails.SetParameterValue("PName", Program)
        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub CHARTDUEPROGRAMS(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New ChartDuePrograms
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        Else
            RepComm = "Total Due in  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.Vw_DuePrpgramTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.Vw_DuePrpgram, YearCode, "Active")
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Total Due in  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub



    Public Sub IntakeSummaryReport(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New IntakeSummaryWOQReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Intake Summary  " & YearCode & " as on " & Dt
        Else
            RepComm = "Intake Summary  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Intake Summary  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Intake Summary  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function IntakeSummaryReportPdf(ByVal UserType As Integer, ByVal UN As String, ByVal YearCode As String, ByVal Dt As String, ByVal FY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New IntakeSummaryWOQReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' Dim strdt As Date = Dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        If FY = "Current Financial Year" Then
            RepComm = "Current Intake Summary  " & YearCode & " as on " & Dt
        Else
            RepComm = "Intake Summary  " & YearCode & " as on " & Dt
        End If
        Dim Da As New FeesTableAdapters.tbl_BudgetTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.tbl_Budget, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        If FY = "Current Financial Year" Then
            'RepComm = "Current Financial Year  " & YearCode & " as on " & Dt
            frm.Text = "Current Intake Summary  " & YearCode & " as on " & Dt
        Else
            'RepComm = "Financial Year Closing  " & YearCode & " as on " & Dt
            frm.Text = "Intake Summary  " & YearCode & " as on " & Dt
        End If


        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        'CryStudentdetails.SetParameterValue("PName", Program)

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub FYCReport(ByVal UserType As Integer, ByVal Program As String, ByVal UN As String, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFYC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Financial Year Closing"
        Dim Da As New FeesTableAdapters.tbl_fees_FYCTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_fees_FYC, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Financial Year Closing"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("PName", Program)
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()

    End Sub


    Public Sub StudentYearSummary(ByVal UserType As Integer, ByVal Year As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_PayStatusTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_PayStatus, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary Courses"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentAllYearSummary(ByVal UserType As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllCourseSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Year Wise Course Summary Details"
        Dim Da As New FeesTableAdapters.Vw_PayStatusTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYears(Ds.Vw_PayStatus)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Total Year Course Summary Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentCourseSummary(ByVal UserType As Integer, ByVal Course As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCourseSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Course Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_CourseSummaryTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByCourseId(Ds.Vw_CourseSummary, Course)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary Courses"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub CourseSummary(ByVal UserType As Integer, ByVal Course As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearCourse
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_YearCourseTotalTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByCourseId(Ds.Vw_YearCourseTotal, Course)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Students Fee Course"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)
        frm.Show()

    End Sub
    Public Sub StudentQuotaSummary(ByVal UserType As Integer, ByVal Quota As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptQuotaSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_QuotaSummaryTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_QuotaSummary, Quota)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub YearQuotaSummary(ByVal UserType As Integer, ByVal Quota As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllQuotaSummary
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_YearWiseQuotaSummaryTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuotaYear(Ds.Vw_YearWiseQuotaSummary, Quota)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Students Fee Course"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)
        frm.Show()

    End Sub
    Public Sub YearQuotaDetails(ByVal UserType As Integer, ByVal Quota As String, ByVal Year As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuota
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_QuotaSummaryTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuotaYear(Ds.Vw_QuotaSummary, Quota, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub YearQuotaSummarydetails(ByVal UserType As Integer, ByVal Year As Integer, ByVal QuotaId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptyearQuotaSummaryDetails
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = text & "Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_YearWiseQuotaSummaryTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuotaSummary(Ds.Vw_YearWiseQuotaSummary, Year, QuotaId)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary Courses"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BatchCourseConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal Program As String, ByVal Course As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "" & "  Batch Course Consolidate "
        Dim Da As New FeesTableAdapters.Vw_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaSRConsolidate As New FeesTableAdapters.Vw_SRConsolidateTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByCourse(Ds.Vw_Consolidate, Batch, Program, Course)
        DaCollege.Fill(Ds.Vw_College)
        DaSRConsolidate.FillByBatch(Ds.Vw_SRConsolidate, Batch)
        CryStudentdetails.Subreports.Item("rptSRConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_SRConsolidate"))

        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Course Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BatchQuotaConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal Program As String, ByVal Quota As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        Dim Qta As String = ""

        If (Quota = "MGMT") Then
            Qta = "Management"
        Else
            Qta = Quota
        End If
        RepComm = Batch & " " & Qta & "  Quota Consolidate"
        Dim Da As New FeesTableAdapters.Vw_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaSRConsolidate As New FeesTableAdapters.Vw_SRConsolidateTableAdapter

        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_Consolidate, Batch, Program, Quota)
        DaCollege.Fill(Ds.Vw_College)

        DaSRConsolidate.FillByBatch(Ds.Vw_SRConsolidate, Batch)
        CryStudentdetails.Subreports.Item("rptSRConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_SRConsolidate"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Quota Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BatchEntryConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal EntryId As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        Dim Entry As String

        If (EntryId = 1) Then
            Entry = "Direct"
        ElseIf (EntryId = 2) Then
            Entry = "Lateral"
        Else
            Entry = "Transfer"
        End If
        RepComm = Batch & "" & Entry & " Entry Consolidate"
        Dim Da As New FeesTableAdapters.Vw_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaSRConsolidate As New FeesTableAdapters.Vw_SRConsolidateTableAdapter

        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEntry(Ds.Vw_Consolidate, Batch, EntryId)
        DaCollege.Fill(Ds.Vw_College)

        DaSRConsolidate.FillByBatch(Ds.Vw_SRConsolidate, Batch)
        CryStudentdetails.Subreports.Item("rptSRConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_SRConsolidate"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch entry Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BatchStudentConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal StudentId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "" & " Student Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaSRConsolidate As New FeesTableAdapters.Vw_SRConsolidateTableAdapter

        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudent(Ds.Vw_Consolidate, Batch, StudentId)
        DaCollege.Fill(Ds.Vw_College)

        DaSRConsolidate.FillByBatch(Ds.Vw_SRConsolidate, Batch)
        CryStudentdetails.Subreports.Item("rptSRConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_SRConsolidate"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BatchYearConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchYearConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_Vw_YCQE_YearConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_Vw_YCQE_YearConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearCourseConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchYearCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Year Course Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YCQE_CourseConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_YCQE_CourseConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Year Course Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub StudentScholarship(ByVal UserType As Integer, ByVal StudentId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentScholarship
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = " Scholarship Report"
        Dim Da As New FeesTableAdapters.Vw_StudentSRTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudentId(Ds.Vw_StudentSR, StudentId)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Scholarship Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearQuotaConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchYearQuotaConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Quota Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YCQE_QuotaConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_YCQE_QuotaConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub



    Public Sub BatchYearEntryConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchYearEntryConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Entry Consolidate"
        Dim Da As New FeesTableAdapters.Vw_YCQE_EntryConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_YCQE_EntryConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch entry Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
    End Sub


    Public Sub SRBatchYearConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSRYearConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Scholarship Consolidate"
        Dim Da As New FeesTableAdapters.Vw_SRYearConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_SRYearConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Year Scholarship Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
    End Sub

    Public Sub SRBatchYearCourseConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSRCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Course Scholarship Consolidate"
        Dim Da As New FeesTableAdapters.Vw_SRCourseConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_SRCourseConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Year Course Scholarship Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
    End Sub

    Public Sub SRBatchYearStudentConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSRYearStudentConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Year Student Scholarship Consolidate"
        Dim Da As New FeesTableAdapters.Vw_SRStudentConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_SRStudentConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Year Student Scholarship Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
    End Sub

    Public Sub SRBatchStudentConsolidate(ByVal UserType As Integer, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptSRBatchStudentConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Student Scholarship Consolidate"
        Dim Da As New FeesTableAdapters.Vw_SRBatchStudentConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_SRBatchStudentConsolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Student Scholarship Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
    End Sub

    Public Sub BatchCourseStudentConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchCourseConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Course Consolidate "
        Dim Da As New FeesTableAdapters.Vw_ConsolidateTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaSRConsolidate As New FeesTableAdapters.Vw_SRConsolidateTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_Consolidate, Batch)
        DaCollege.Fill(Ds.Vw_College)
        DaSRConsolidate.FillByBatch(Ds.Vw_SRConsolidate, Batch)
        CryStudentdetails.Subreports.Item("rptSRConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_SRConsolidate"))

        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Course Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchTallyReport(ByVal UserType As Integer, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTallyReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Tally Report "

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaTally As New FeesTableAdapters.Vw_TallyReportTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        DaCollege.Fill(Ds.Vw_College)
        DaTally.FillByBatch(Ds.Vw_TallyReport, Batch)

        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Tally Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchStudentYearCourseConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchStudentYearWise
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Student Year Course Consolidate"
        Dim Da As New FeesTableAdapters.Vw_YCQE_NOSTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_YCQE_NOS, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Batch Year Course Scholarship Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails

        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(UserType)

        frm.Show()
    End Sub
    Public Sub YearQuotaCategoryDetailsTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTAC, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub YearQuotaCategoryDetailsTFOFCurrent(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearCurrent(Ds.Vw_OTAC, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function YearQuotaCategoryDetailsTFOFCurrentPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearCurrent(Ds.Vw_OTAC, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function
    Public Sub YearQuotaCategoryDetailsOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryOF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTACOF, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function YearQuotaCategoryDetailsOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryOF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTACOF, Year, Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub YearQuotaCategoryDetailsTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryTF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTACTF, Program, Year, Batch, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function YearQuotaCategoryDetailsTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryTF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_OTACTF, Program, Year, Batch, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function
    'Year Reports

    Public Sub BYStudentTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByDue(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentDue(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function BYStudentTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByDue(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentDue(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function

    Public Sub BYStudentTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByDue(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentDue(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function BYStudentTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByDue(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentDue(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function

    Public Sub BYSTTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFSTPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByStudent(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentStudent(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function BYSTTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFSTPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByStudent(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentStudent(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function
    Public Sub BYConvenorTFAD(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDuesConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch All Year Due List"
        Dim Da As New FeesTableAdapters.Vw_YearAllDuesTableAdapter
        Dim Da1 As New FeesTableAdapters.Vw_OTACTF1TableAdapter
        Dim Da2 As New FeesTableAdapters.Vw_OTACTF2TableAdapter
        Dim Da3 As New FeesTableAdapters.Vw_OTACTF3TableAdapter
        Dim Da4 As New FeesTableAdapters.Vw_OTACTF4TableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByBatchConvenor(Ds.Vw_YearAllDues, Batch, Status)
        Da1.FillByDue(Ds.Vw_OTACTF1, Batch, 1)
        Da2.FillByDue(Ds.Vw_OTACTF2, Batch, 2)
        Da3.FillByDue(Ds.Vw_OTACTF3, Batch, 3)
        Da4.FillByDue(Ds.Vw_OTACTF4, Batch, 4)

        DaCollege.Fill(Ds.Vw_College)

        'CryStudentdetails.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF1"))
        'CryStudentdetails.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF2"))
        'CryStudentdetails.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF3"))
        'CryStudentdetails.Subreports.Item("rptFourthYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF4"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch All Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BYAPSMFCTFAD(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDuesA
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch All Year Due List"
        Dim Da As New FeesTableAdapters.Vw_YearAllDuesTableAdapter
        Dim Da1 As New FeesTableAdapters.Vw_OTACTF1TableAdapter
        Dim Da2 As New FeesTableAdapters.Vw_OTACTF2TableAdapter
        Dim Da3 As New FeesTableAdapters.Vw_OTACTF3TableAdapter
        Dim Da4 As New FeesTableAdapters.Vw_OTACTF4TableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByBatchAPSMFC(Ds.Vw_YearAllDues, Batch, Status)
        Da1.FillByDue(Ds.Vw_OTACTF1, Batch, 1)
        Da2.FillByDue(Ds.Vw_OTACTF2, Batch, 2)
        Da3.FillByDue(Ds.Vw_OTACTF3, Batch, 3)
        Da4.FillByDue(Ds.Vw_OTACTF4, Batch, 4)

        DaCollege.Fill(Ds.Vw_College)

        'CryStudentdetails.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF1"))
        'CryStudentdetails.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF2"))
        'CryStudentdetails.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF3"))
        'CryStudentdetails.Subreports.Item("rptFourthYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF4"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch All Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BYStudentTFAD(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDues
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch All Year Due List"
        Dim Da As New FeesTableAdapters.Vw_YearAllDuesTableAdapter
        Dim Da1 As New FeesTableAdapters.Vw_OTACTF1TableAdapter
        Dim Da2 As New FeesTableAdapters.Vw_OTACTF2TableAdapter
        Dim Da3 As New FeesTableAdapters.Vw_OTACTF3TableAdapter
        Dim Da4 As New FeesTableAdapters.Vw_OTACTF4TableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByADStudents(Ds.Vw_YearAllDues, Batch, Status)
        Da1.FillByDue(Ds.Vw_OTACTF1, Batch, 1)
        Da2.FillByDue(Ds.Vw_OTACTF2, Batch, 2)
        Da3.FillByDue(Ds.Vw_OTACTF3, Batch, 3)
        Da4.FillByDue(Ds.Vw_OTACTF4, Batch, 4)

        DaCollege.Fill(Ds.Vw_College)

        'CryStudentdetails.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF1"))
        'CryStudentdetails.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF2"))
        'CryStudentdetails.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF3"))
        'CryStudentdetails.Subreports.Item("rptFourthYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF4"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch All Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BYMGMTTFAD(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDues
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch All Year Due List"
        Dim Da As New FeesTableAdapters.Vw_YearAllDuesTableAdapter
        Dim Da1 As New FeesTableAdapters.Vw_OTACTF1TableAdapter
        Dim Da2 As New FeesTableAdapters.Vw_OTACTF2TableAdapter
        Dim Da3 As New FeesTableAdapters.Vw_OTACTF3TableAdapter
        Dim Da4 As New FeesTableAdapters.Vw_OTACTF4TableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByADMgmt(Ds.Vw_YearAllDues, Batch, Status)
        Da1.FillByDue(Ds.Vw_OTACTF1, Batch, 1)
        Da2.FillByDue(Ds.Vw_OTACTF2, Batch, 2)
        Da3.FillByDue(Ds.Vw_OTACTF3, Batch, 3)
        Da4.FillByDue(Ds.Vw_OTACTF4, Batch, 4)

        DaCollege.Fill(Ds.Vw_College)

        'CryStudentdetails.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF1"))
        'CryStudentdetails.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF2"))
        'CryStudentdetails.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF3"))
        'CryStudentdetails.Subreports.Item("rptFourthYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF4"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch All Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BYUFROTFAD(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptAllYearDues
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch All Year Due List"
        Dim Da As New FeesTableAdapters.Vw_YearAllDuesTableAdapter
        Dim Da1 As New FeesTableAdapters.Vw_OTACTF1TableAdapter
        Dim Da2 As New FeesTableAdapters.Vw_OTACTF2TableAdapter
        Dim Da3 As New FeesTableAdapters.Vw_OTACTF3TableAdapter
        Dim Da4 As New FeesTableAdapters.Vw_OTACTF4TableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByADUFRO(Ds.Vw_YearAllDues, Batch, Status)
        Da1.FillByDue(Ds.Vw_OTACTF1, Batch, 1)
        Da2.FillByDue(Ds.Vw_OTACTF2, Batch, 2)
        Da3.FillByDue(Ds.Vw_OTACTF3, Batch, 3)
        Da4.FillByDue(Ds.Vw_OTACTF4, Batch, 4)

        DaCollege.Fill(Ds.Vw_College)

        'CryStudentdetails.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF1"))
        'CryStudentdetails.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF2"))
        'CryStudentdetails.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF3"))
        'CryStudentdetails.Subreports.Item("rptFourthYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_OTACTF4"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch All Year Due List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub


    Public Sub BYConvenorTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Convenor List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByConvenor(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From Convenor List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYConvenorTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Convenor List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByConvenor(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From Convenor List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function
    Public Sub BYConvenorTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Convenor List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByConvenor(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From Convenor List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYConvenorTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Convenor List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByConvenor(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From Convenor List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function



    Public Sub BYAPSMFCTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From APSMFC List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False
        If CY = "CurrentStatus" Then
            Da.FillByCAPSMFC(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByAPSMFC(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & " Year Due From APSMFC List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()

    End Sub

    Public Function BYAPSMFCTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From APSMFC List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False
        If CY = "CurrentStatus" Then
            Da.FillByCAPSMFC(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByAPSMFC(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & " Year Due From APSMFC List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub BYAPSMFCTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From APSMFC List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False
        If CY = "CurrentStatus" Then
            Da.FillByCAPSMFC(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByAPSMFC(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & " Year Due From APSMFC List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYAPSMFCTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From APSMFC List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Ds.EnforceConstraints = False
        If CY = "CurrentStatus" Then
            Da.FillByCAPSMFC(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByAPSMFC(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & " Year Due From APSMFC List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub BYUFROTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From UFRO List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If CY = "CurrentStatus" Then
            Da.FillByCUFRO(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByUFRO(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From UFRO List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYUFROTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From UFRO List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If CY = "CurrentStatus" Then
            Da.FillByCUFRO(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByUFRO(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From UFRO List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub BYUFROTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From UFRO List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If CY = "CurrentStatus" Then
            Da.FillByCUFRO(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByUFRO(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From UFRO List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()

    End Sub

    Public Function BYUFROTFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From UFRO List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If CY = "CurrentStatus" Then
            Da.FillByCUFRO(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        Else
            Da.FillByUFRO(Ds.Vw_OTACTFOF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Due From UFRO List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub DateBase(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal UN As String, ByVal CY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptDateBaseYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "Year Student Details"
        Dim Da As New FeesTableAdapters.Vw_DateBaseTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If CY = "CurrentStatus" Then
            Da.FillByCurrentDateBase(Ds.Vw_DateBase, Batch, Year, Program, Status)
        Else
            Da.FillByDateBase(Ds.Vw_DateBase, Batch, Year, Program, Status)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year Student Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()

    End Sub


    Public Sub BYSTTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & " Year Due From Student List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByStudent(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentStudent(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due From Student List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function BYSTTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String, ByVal CS As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & " Year Due From Student List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        If CS = "Year Status" Then
            Da.FillByStudent(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        Else
            Da.FillByCurrentStudent(Ds.Vw_OTACTF, Batch, Year, Status, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & " Batch " & Year & "th Year Due From Student List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function

    'Public Sub BYSTTFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
    '    frm.crViewer.ReportSource = Nothing
    '    frm.crViewer.Refresh()
    '    Dim CryStudentdetails As New rptBYTFStudentPayableStudents
    '    Dim Ds As New Fees
    '    Dim RepComm As String = ""
    '    'YearName(Year)
    '    RepComm = Batch & " Batch " & Year & " Year Due From Student List"
    '    Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
    '    Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
    '    Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
    '    Ds.EnforceConstraints = False
    '    DaLogo.Fill(Ds.Vw_Logo)
    '    Da.FillByStudent(Ds.Vw_OTAC, Batch, Year, Status, Program)
    '    DaCollege.Fill(Ds.Vw_College)
    '    CryStudentdetails.SummaryInfo.ReportComments = RepComm

    '    CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
    '    frm.Text = Batch & " Batch " & Year & "th Year Due From Student List"
    '    frm.crViewer.DisplayGroupTree = True
    '    frm.crViewer.ReportSource = CryStudentdetails
    '    EnableButtons(UserType)
    '    frm.Show()
    'End Sub

    Public Sub BYQUOTATF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Quota As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Student  List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_OTACTF, Batch, Year, Quota, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year " & Quota & " Due From Student  List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYQUOTATFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Quota As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Student  List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_OTACTF, Batch, Year, Quota, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year " & Quota & " Due From Student  List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub BYQUOTATFOF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Quota As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Student  List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_OTACTFOF, Batch, Year, Quota, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year " & Quota & " Due From Student  List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function BYQUOTATFOFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Quota As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYTFOFStudentPayableStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch " & Year & "th Year Due From Student  List"
        Dim Da As New FeesTableAdapters.Vw_OTACTFOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByQuota(Ds.Vw_OTACTFOF, Batch, Year, Quota, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = Batch & "Batch " & Year & " Year " & Quota & " Due From Student  List"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function

    'End Year Reports
    Public Sub YearQuotaCategoryDetailsWOR(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTAC, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function YearQuotaCategoryDetailsWORPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategory
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTAC, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function
    Public Sub YearQuotaCategoryDetailsWORTF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryTF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTACTF, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function YearQuotaCategoryDetailsWORTFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryTF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTACTF, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        ' frm.Show()
        Return CryStudentdetails

    End Function
    Public Sub YearQuotaCategoryDetailsWOROF(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryOF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTACOF, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function YearQuotaCategoryDetailsWOROFPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYearQuotaCategoryOF
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = "Quota Fee Summary Details"
        Dim Da As New FeesTableAdapters.Vw_OTACOFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearWOR(Ds.Vw_OTACOF, Year, "Yes", Batch, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "List Of Fee Summary QUOTA"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub BOATCCONSOLIDATEWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEPAYABLEWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayableWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchPayableWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEPAYABLEWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayableWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchPayableWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BOATCCONSOLIDATERECEIVEDWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchReceivedWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchReceivedWOR(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATERECEIVEDWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchReceivedWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchReceivedWOR(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCCONSOLIDATERA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATERA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEPAYABLERA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayableRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchPayableRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEPAYABLERA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayableRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchPayableRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")


        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATERECEIVEDRA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchReceivedRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchReceivedRA(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATERECEIVEDRA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate with Out ReAdmission"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchReceivedRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA", Status)
        Else
            Da.FillByBBatchReceivedRA(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, "RA")

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate with Out ReAdmission"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCCONSOLIDATE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)    
        Dim CryStudentdetails As New rptBatchOATCCon
        RepComm = "Batch" & Batch & "Total Students"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatch(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEENTRYMODE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal Entry As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)    
        Dim CryStudentdetails As New rptBatchOATCCon
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        If Status <> "Both" Then
            Da.FillByBatchEntry(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status, Entry)
        Else
            Da.FillByBBatchEntry(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Entry)

        End If

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCTFCONSOLIDATE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)    
        Dim CryStudentdetails As New rptBatchOATCActualCon
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatch(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEENTRYMODE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal Entry As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)    
        Dim CryStudentdetails As New rptBatchOATCActualCon
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        If Status <> "Both" Then
            Da.FillByBatchEntry(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status, Entry)
        Else
            Da.FillByBBatchEntry(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Entry)

        End If

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEPAYABLES(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayable(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPayable(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEPAYABLES(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTFStudentPayable
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayable(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPayable(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEPAYABLESA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchAPSMFC(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchAPSMFC(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCCONSOLIDATEPAYABLESC(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchConvenor(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchConvenor(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATEPAYABLESA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTFStudentPayableAPSMFC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchAPSMFC(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchAPSMFC(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub



    Public Sub BOATCTFCONSOLIDATEPAYABLESC(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTFStudentPayableConvenor
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchConvenor(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchConvenor(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub





    Public Sub BOATCCONSOLIDATEPAYABLESS(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPaybleStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchStudent(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchStudent(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCCONSOLIDATEPAYABLESU(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentPaybleUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchUFRO(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchUFRO(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub



    Public Sub BOATCTFCONSOLIDATEPAYABLESS(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTFStudentPaybleStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchStudent(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchStudent(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCTFCONSOLIDATEPAYABLESU(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptTFStudentPaybleUFRO
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchUFRO(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchUFRO(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BOATCCONSOLIDATEPAYABLE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayable(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPayable(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BOATCTFCONSOLIDATEPAYABLE(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPayable(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPayable(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub BOATCCONSOLIDATERECEIVED(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPaid(Ds.Vw_BATCHOATCCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPaid(Ds.Vw_BATCHOATCCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BOATCTFCONSOLIDATERECEIVED(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchOATCActualCon
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & "Batch Student Consolidate"
        Dim Da As New FeesTableAdapters.Vw_BATCHOATCTFCONSOLIDATETableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatchPaid(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch, Status)
        Else
            Da.FillByBBatchPaid(Ds.Vw_BATCHOATCTFCONSOLIDATE, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub




    Public Sub BatchYearQuotaCategoryConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCCON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCCONTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCCON, Batch, Status, Program)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCCON, Batch, Program)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearCategoryConsolidate(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal Program As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCCONCAT
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch  Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCCATCONTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCCATCON, Batch, Status, Program)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCCATCON, Batch, Program)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub Intake(ByVal UserType As Integer, ByVal Batch As String, ByVal Year As Integer, ByVal Program As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptIntake
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        RepComm = Batch & " Batch Intake Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_IntakeReportTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)

        Da.FillByBatch(Ds.Vw_IntakeReport, Batch, Year, Program)

        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Intake Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearQuotaCategoryConsolidateTF(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCTFCON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCTFCONTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCTFCON, Batch, Status, Program)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCTFCON, Batch, Program)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub




    Public Sub BatchYearQuotaCategoryConsolidateWOR(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCWORCON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCCONWORATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCCONWORA, Batch, Status)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCCONWORA, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearQuotaCategoryConsolidateWORTF(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCTFWORCON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCTFCONWORATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCTFCONWORA, Batch, Status)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCTFCONWORA, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Sub BatchYearQuotaCategoryConsolidateRA(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCRACON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCCONRATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCCONRA, Batch, Status)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCCONRA, Batch)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub BatchYearQuotaCategoryConsolidateRATF(ByVal UserType As Integer, ByVal Batch As String, ByVal Status As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBYQCTFRACON
        Dim Ds As New Fees
        Dim RepComm As String = ""
        'YearName(Year)
        ' RepComm = Batch & " Batch Year Consolidate Report"
        Dim Da As New FeesTableAdapters.Vw_YQCTFCONRATableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If Status <> "Both" Then
            Da.FillByBatch(Ds.Vw_YQCTFCONRA, Batch, Status)
        Else
            Da.FillByBatchAI(Ds.Vw_YQCTFCONRA, Batch)
        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Batch Student Consolidate Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)
        frm.Show()

    End Sub


    Public Sub YearQuotaCategoryDetails(ByVal UserType As Integer, ByVal Year As Integer, ByVal QCId As String, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYQCList
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = QCId & " Student Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYQC(Ds.Vw_OTAC, Year, QCId, Batch, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = QCId & " Student Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub TFYearQuotaCategoryDetails(ByVal UserType As Integer, ByVal Year As Integer, ByVal QCId As String, ByVal Batch As String, ByVal Status As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptYQCTFList
        Dim Ds As New Fees
        Dim RepComm As String = ""
        YearName(Year)
        RepComm = QCId & " Student Details"
        Dim Da As New FeesTableAdapters.Vw_OTACTFTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYQC(Ds.Vw_OTACTF, Year, QCId, Batch, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = QCId & " Student Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub


    Public Sub FinancialYear(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancial
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function FinancialYearPdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancial
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub FinancialYearQuotaWise(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialQuota
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Financial Year Due List  " & DT
        ' RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function FinancialYearQuotaWisePdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialQuota
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Financial Year Due List  " & DT
        ' RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub FinancialYearQuotaWiseTOF(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialQuotaTOF
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Financial Year Due List  " & DT
        '  RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        'CryStudentdetails.SetParameterValue("PName ", PName)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Function FinancialYearQuotaWiseTOFPdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal DT As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialQuotaTOF
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Financial Year Due List  " & DT
        '  RepComm = DT
        Dim Da As New FeesTableAdapters.tbl_fees_QFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        'CryStudentdetails.SetParameterValue("PName ", PName)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub FinancialYearDue(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinanciaYearDue
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode & " FINANCIAL YEAR"
        Dim Da As New FeesTableAdapters.tbl_fees_FinancialYearDueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFY(Ds.tbl_fees_FinancialYearDue, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = YearCode & " FINANCIAL YEAR"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        'CryStudentdetails.SetParameterValue("PName ", PName)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub FinancialYearMonthWise(ByVal UserType As Integer, ByVal YearCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthFinancial
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode
        Dim Da As New FeesTableAdapters.tbl_fees_QMonthsFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QMonthsFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub FinancialYearReceived(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialReceived
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FinancialRPTReceivedTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.Vw_FinancialRPTReceived)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("Program", Program)


        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Function FinancialYearReceivedPdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialReceived
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FinancialRPTReceivedTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.Vw_FinancialRPTReceived)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("Program", Program)


        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails
    End Function

    Public Sub FinancialYearTally(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFYTally
        Dim Ds As New Fees
        Dim RepComm As String = "Financial Year Tally Up to " & Arr
        '  RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FYTallyTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_FYTally)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Year Tally"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)


        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub FinancialYearReconsilationCategory(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCategoryReconsilation
        Dim Ds As New Fees
        Dim RepComm As String = YearCode & " Category Financial Received Report"
        ' RepComm = Arr
        Dim Da As New FeesTableAdapters.tbl_fees_ReconsilationTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_Reconsilation)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Category Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("Program", Program)


        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub FinancialYearQuotaCategoryConsolidate(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptQuotaJEConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = "Academic Year Quota Category Consolidate"
        Dim Da As New FeesTableAdapters.Vw_YPCTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYPC(Ds.Vw_YPC, YearCode)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Academic Year Quota Category Consolidate"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub FinancialYearReceivedAll(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialReceivedAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FinancialRPTReceivedTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.Vw_FinancialRPTReceived)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("PName", PName)
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Public Function FinancialYearReceivedAllPdf(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFinancialReceivedAll
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FinancialRPTReceivedTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByFinancialYear(Ds.Vw_FinancialRPTReceived)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        CryStudentdetails.SetParameterValue("PName", PName)
        EnableButtons(UserType)

        'frm.Show()
        Return CryStudentdetails

    End Function

    Public Sub FinancialYearReceivedAllReconsilation(ByVal UserType As Integer, ByVal YearCode As String, ByVal Arr As String, ByVal UN As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFRReconsilation
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Arr
        Dim Da As New FeesTableAdapters.Vw_FRReconsiliationTableAdapter
        'Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        'Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_FRReconsiliation)
        'DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Received Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        'CryStudentdetails.SetParameterValue("UserName", UN)
        'CryStudentdetails.SetParameterValue("PName", PName)
        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub FinancialYearMonthWiseConsolidate(ByVal UserType As Integer, ByVal YearCode As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptMonthFinancialConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = YearCode
        Dim Da As New FeesTableAdapters.tbl_fees_QMonthsFinancialTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_QMonthsFinancial)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()

    End Sub
    Public Sub StudentMarksReport(ByVal UserType As Integer, ByVal Sid As String, ByVal Sem As String, ByVal year As Integer, ByVal Var_Promote As String, ByVal TP As String, ByVal PP As String)

        Try
            frm.crViewer.ReportSource = Nothing
            frm.crViewer.Refresh()
            Dim CryStudentdetails As New rptStudentMarksDetails
            Dim Ds As New Fees
            Dim RepComm As String = ""
            'YearName(Year)
            RepComm = "StudentMarksReport"
            ' Dim Das As New FeesTableAdapters.Vw_StudentElementsTableAdapter
            ' Dim DSR As New FeesTableAdapters.Vw_StudentResultTableAdapter
            Dim Da As New FeesTableAdapters.Vw_StudentMarksDetailsTableAdapter
            Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
            Ds.EnforceConstraints = False
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByStudentMarks(Ds.Vw_StudentMarksDetails, Sid, Sem, year)

            ' Da.FillByStudent(Ds.Vw_StudentElements, Sid)
            DaCollege.Fill(Ds.Vw_College)
            ' DSR.FillByMarks(Ds.Vw_StudentResult, Sid, Sem, year)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            CryStudentdetails.SetParameterValue("Promote", Var_Promote)
            CryStudentdetails.SetParameterValue("TP", TP)
            CryStudentdetails.SetParameterValue("PP", PP)
            frm.Text = "List Of SubjectMarks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub BatchTotals(ByVal UserType As Integer, ByVal RepName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptBatchTotals
        Dim Ds As New Fees
        Dim RepComm As String = ""

        RepComm = RepName & " " & "  Receivable Totals"
        Dim Da As New FeesTableAdapters.tbl_fees_BatchTotalsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_BatchTotals)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        '

        '

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))

        CryStudentdetails.SetParameterValue("RepName", RepName)

        frm.Text = text & "Financial Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()

    End Sub


    Private Sub YearName(ByVal year As Integer)
        If year = 1 Then
            text = year & "st Year "
        ElseIf year = 2 Then
            text = year & "nd Year "
        ElseIf year = 3 Then
            text = year & "rd Year "
        ElseIf year = 4 Then
            text = year & "th Year "
        End If

    End Sub


    Private Sub EnableButtons(ByVal UserType As String)
        If UserType = 3 Then

            frm.crViewer.EnableToolTips = False
            frm.crViewer.ShowPrintButton = False
            frm.crViewer.ShowExportButton = False
        End If
    End Sub


End Class
