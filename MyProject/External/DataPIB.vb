﻿Imports System.Data.OleDb
Imports System.Management
Imports System.Text.RegularExpressions

Public Class DataPIB
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, ErrMsg2, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Synchronize PIB"
    Dim mac1, mada As String

    Private Sub ProcessFile(ByVal FileName As String)
        Dim OleConn As OleDbConnection
        Dim OleReader, OleReaderA, OleReaderB, OleReaderC As OleDbDataReader
        Dim RstQ0, RstQA, RstQB As DataTableReader

        Dim strsql As String
        Dim BCAJU, BCPIB, BCSPPB As String
        Dim xShipNo, xBL, xHOSTBL, xSUP, xSUPNM, xAJU, xPIB, xSPPB, xVessel, xDELDT As String
        Dim xRESKRIP, xRESDES As String
        Dim DCKD, DCNO As String
        Dim BCPIB_DT, BCSPPB_DT, DCDT As Date
        Dim iLoop, irec, xContTot, iChk As Integer
        Dim KeyBL As String
        Dim dCAR, mCAR, mFLAG, mMIN_DATE, mMAX_DATE, xCAR, yCAR, xDOKKD, xDOKNO, xPIBNO, xDOKRESNO, xRESKD, xRESWK, xKPBC, xDESKRIPSI, xCONTNO, xCONTUKUR, xCONTTIPE As String
        Dim xDOKTG, xPIBTG, xDOKRESTG, xRESTG As Date




        ' Error Handling Variables
        Dim strTmp As String

        Dim FileNm As String
        FileNm = FileName

        SQLstr = "SELECT DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -6 MONTH),'%Y-%m-%d') tgl1, DATE_FORMAT(DATE_ADD(NOW(), INTERVAL -1 MONTH),'%Y-%m-%d') tgl2"
        RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not RstQB Is Nothing Then
            While RstQB.Read
                Try
                    mMIN_DATE = RstQB.GetValue(0)
                Catch ex As Exception
                    mMIN_DATE = Nothing
                End Try
                Try
                    mMAX_DATE = RstQB.GetValue(1)
                Catch ex As Exception
                    mMAX_DATE = Nothing
                End Try
            End While
        End If
        RstQB.Close()


        'supram closed
        SQLstr = "" & _
            "SELECT tblPibDok.CAR, tblPibDok.DOKKD, tblPibDok.DOKNO, tblPibDok.DOKTG " & _
            "From tblPibDok " & _
            "Where (tblPibDok.DokKd = '705' or tblPibDok.DokKd = '704') " & _
            "   And format(tblPibDok.DokTg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' order by tblPibDok.CAR asc"
        ' tblPibDok.CAR = '07000000066020191125000707' and 
        OleReader = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
        If Not OleReader Is Nothing Then
            While OleReader.Read()
                Try
                    xCAR = ReplStr(OleReader.GetString(0))
                Catch ex As Exception
                    xCAR = ""
                End Try

                Try
                    xDOKKD = ReplStr(OleReader.GetString(1))
                Catch ex As Exception
                    xDOKKD = ""
                End Try

                Try
                    xDOKNO = ReplStr(OleReader.GetString(2))
                Catch ex As Exception
                    xDOKNO = ""
                End Try

                Try
                    xDOKTG = OleReader.GetDateTime(3)
                Catch ex As Exception
                    xDOKTG = Nothing
                End Try
                If xCAR <> "" Then
                    MyReader = Nothing
                    mCAR = ""
                    mFLAG = ""
                    SQLstr = "" & _
                        "select CAR, FLAG from pib_tblPibDok where car = '" & xCAR & "'"
                    MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            Try
                                mCAR = MyReader.GetString(0)
                            Catch ex As Exception
                                mCAR = ""
                            End Try
                            Try
                                mFLAG = MyReader.GetString(1)
                            Catch ex As Exception
                                mFLAG = ""
                            End Try
                        End While
                        'CloseMyReader(MyReader, UserData)
                        MyReader.Close()
                    End If
                    'If mFLAG = "" Then
                    If mFLAG = mFLAG Then
                        If mCAR <> "" Then
                            ErrMsg = "Failed when read Users Data"
                            SQLstr = "delete from pib_TBLPIBHDR where car = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                            SQLstr = "delete from PIB_TBLPIBRES where car = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                            SQLstr = "delete from PIB_TBLPIBCON where car = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                            SQLstr = "delete from pib_tblPibDok where CAR = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                            SQLstr = "delete from pib_tblpibdokall where CAR = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                            SQLstr = "delete from tbl_pib_history where aju_no = '" & mCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                        End If

                        SQLstr = "" & _
                            " insert into pib_tblPibDok (" & _
                            "    CAR, DOKKD, DOKNO, DOKTG" & _
                            " ) VALUES (" & _
                            "    '" & xCAR & "', '" & xDOKKD & "', '" & xDOKNO & "', '" & Format(xDOKTG, "yyyy-MM-dd hh:mm:ss") & "'" & _
                            " )"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        If affrow < 0 Then
                            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                            Exit Sub
                        End If

                        ' supram
                        strsql = " SELECT tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK,  tblPibRes.KPBC, tblTabel.URAIAN, tblPibRes.DesKripsi " & _
                                 " FROM tblPibRes, tblTabel Where tblPibRes.RESKD = tblTabel.KDREC " & _
                                 " And tblPibRes.CAR = '" & xAJU & "' " & _
                                 " ORDER BY tblPibRes.CAR, tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK "
                        OleReaderA = Nothing
                        OleReaderA = DBQueryOleReader(strsql, OleConn, "", FileNm)
                        iLoop = 1
                        If Not OleReaderA Is Nothing Then
                            While OleReaderA.Read()
                                Try
                                    xRESKD = ReplStr(OleReaderA.GetString(0))
                                    xRESTG = OleReaderA.GetDateTime(1)
                                    xRESWK = OleReaderA.GetString(2)
                                    xKPBC = ReplStr(OleReaderA.GetString(3))
                                    xRESKRIP = ReplStr(Trim(OleReaderA.GetString(4)))
                                    If xRESKRIP <> "" Then xRESKRIP = "[NOTES]: " & xRESKRIP & ""
                                    xRESDES = ReplStr(Trim(OleReaderA.GetString(5))) & " " & xRESKRIP

                                Catch ex As Exception
                                    xRESKD = ""
                                    xRESTG = Nothing
                                    xRESWK = ""
                                    xKPBC = ""
                                    xRESKRIP = ""
                                    xRESDES = ""
                                End Try
                                If xRESTG <> Nothing Then
                                    If xRESWK <> "" Then
                                        xRESWK = Mid(xRESWK, 1, 2) & ":" & Mid(xRESWK, 3, 2) & ":" & Mid(xRESWK, 5, 2)
                                    Else
                                        xRESWK = "00:00:00"
                                    End If

                                    xRESTG = xRESTG & " " & xRESWK

                                    SQLstr = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                             & " values ('" & xAJU & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "','" & Format(xRESTG, "yyyy-MM-dd hh:mm:ss") & "')"

                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                End If
                                iLoop = iLoop + 1
                            End While
                        End If
                        OleReaderA.Close()
                        ' end supram

                        OleReaderA = Nothing
                        SQLstr = "" & _
                            "select TBLPIBHDR.PIBNO, TBLPIBHDR.PIBTG from TBLPIBHDR where  TBLPIBHDR.CAR = '" & xCAR & "'"
                        OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
                        If Not OleReaderA Is Nothing Then
                            While OleReaderA.Read()
                                Try
                                    xPIBNO = ReplStr(OleReaderA.GetString(0))
                                Catch ex As Exception
                                    xPIBNO = ""
                                End Try
                                Try
                                    xPIBTG = OleReaderA.GetDateTime(1)
                                Catch ex As Exception
                                    xPIBTG = Nothing
                                End Try
                                If xPIBNO <> "" Then
                                    SQLstr = "" & _
                                            "insert into pib_TBLPIBHDR (" & _
                                            "   CAR, PIBNO, PIBTG" & _
                                            ") VALUES (" & _
                                            "   '" & xCAR & "', '" & xPIBNO & "', '" & Format(xPIBTG, "yyyy-MM-dd hh:mm:ss") & "'" & _
                                            ")"
                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                    If affrow < 0 Then
                                        MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                                        Exit Sub
                                    End If
                                End If
                            End While
                        End If
                        OleReaderA.Close()
                        OleReaderA = Nothing

                        SQLstr = "" & _
                            "select TBLPIBRES.DOKRESNO, TBLPIBRES.DOKRESTG, TBLPIBRES.RESKD, TBLPIBRES.RESTG, TBLPIBRES.RESWK, TBLPIBRES.KPBC, TBLPIBRES.DESKRIPSI " & _
                            "from TBLPIBRES " & _
                            "where  TBLPIBRES.CAR = '" & xCAR & "' and TBLPIBRES.RESKD = '300'"
                        OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
                        If Not OleReaderA Is Nothing Then
                            While OleReaderA.Read()
                                Try
                                    xDOKRESNO = ReplStr(OleReaderA.GetString(0))
                                Catch ex As Exception
                                    xDOKRESNO = ""
                                End Try

                                Try
                                    xDOKRESTG = OleReaderA.GetDateTime(1)
                                Catch ex As Exception
                                    xDOKRESTG = Nothing
                                End Try

                                Try
                                    xPIBNO = ReplStr(OleReaderA.GetString(2))
                                Catch ex As Exception
                                    xPIBNO = ""
                                End Try

                                Try
                                    xRESTG = OleReaderA.GetDateTime(3)
                                Catch ex As Exception
                                    xRESTG = Nothing
                                End Try

                                Try
                                    xRESWK = ReplStr(OleReaderA.GetString(4))
                                Catch ex As Exception
                                    xRESWK = ""
                                End Try

                                Try
                                    xKPBC = ReplStr(OleReaderA.GetString(5))
                                Catch ex As Exception
                                    xKPBC = ""
                                End Try

                                Try
                                    xDESKRIPSI = ReplStr(OleReaderA.GetString(6))
                                Catch ex As Exception
                                    xDESKRIPSI = ""
                                End Try
                                If xRESWK <> "" Then
                                    SQLstr = "" & _
                                        "insert into PIB_TBLPIBRES (" & _
                                        "   DOKRESNO, DOKRESTG, RESKD, CAR, RESTG, RESWK, KPBC, DESKRIPSI" & _
                                        ") VALUES (" & _
                                        "   '" & xDOKRESNO & "', '" & Format(xDOKRESTG, "yyyy-MM-dd hh:mm:ss") & "', '" & xRESKD & "', '" & xCAR & "', '" & Format(xRESTG, "yyyy-MM-dd hh:mm:ss") & "', '" & xRESWK & "', '" & xKPBC & "', '" & xDESKRIPSI & "'" & _
                                        ")"
                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                    If affrow < 0 Then
                                        MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                                        Exit Sub
                                    End If
                                End If
                            End While
                        End If
                        OleReaderA.Close()
                        '
                        SQLstr = "" & _
                            "select TBLPIBCON.CONTNO, TBLPIBCON.CONTUKUR, TBLPIBCON.CONTTIPE " & _
                            "from TBLPIBCON " & _
                            "where TBLPIBCON.CAR = '" & xCAR & "'"
                        OleReaderA = Nothing
                        OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
                        If Not OleReaderA Is Nothing Then
                            While OleReaderA.Read()
                                Try
                                    xCONTNO = ReplStr(OleReaderA.GetString(0))
                                Catch ex As Exception
                                    xCONTNO = ""
                                End Try
                                Try
                                    xCONTUKUR = ReplStr(OleReaderA.GetString(1))
                                Catch ex As Exception
                                    xCONTUKUR = ""
                                End Try
                                Try
                                    xCONTTIPE = ReplStr(OleReaderA.GetString(2))
                                Catch ex As Exception
                                    xCONTTIPE = ""
                                End Try
                                If xCONTNO <> "" Then
                                    SQLstr = "" & _
                                            "insert into PIB_TBLPIBCON (" & _
                                            "   CONTNO, CONTUKUR, CONTTIPE, CAR" & _
                                            ") VALUES (" & _
                                            "   '" & xCONTNO & "', '" & xCONTUKUR & "', '" & xCONTTIPE & "', '" & xCAR & "'" & _
                                            ")"
                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                    If affrow < 0 Then
                                        MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                                        Exit Sub
                                    End If
                                End If
                            End While
                        End If
                        OleReaderA.Close()
                    End If
                End If

            End While
        End If
        OleReader.Close()


        SQLstr = "select count(1) as boleh from tbm_addsyn where userid = '" & UserData.UserCT & "'"
        ErrMsg = "Data add"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)


        If Not MyReader Is Nothing Then
            While MyReader.Read()
                Try
                    mada = MyReader.GetString(0)
                Catch ex As Exception
                    mada = 0
                End Try

            End While
        End If
        MyReader.Close()



        If mada = 1 Then


            'supram end
            yCAR = ""
            SQLstr = "" & _
                " SELECT tblPibDok.CAR, tblPibDok.DOKKD, tblPibDok.DOKNO, tblPibDok.DOKTG " & _
                " From tblPibDok, tblpibres  " & _
                " where tblPibDok.CAR = tblpibres.CAR " & _
                "   and format(tblpibres.dokrestg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' " & _
                " order by tblPibDok.CAR asc"
            OleReader = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
            If Not OleReader Is Nothing Then
                While OleReader.Read()
                    Try
                        xCAR = ReplStr(OleReader.GetString(0))
                    Catch ex As Exception
                        xCAR = ""
                    End Try
                    If yCAR <> xCAR Then
                        yCAR = xCAR
                        SQLstr = "delete from pib_tblpibdokall where CAR = '" & yCAR & "'"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                    End If

                    Try
                        xDOKKD = ReplStr(OleReader.GetString(1))
                    Catch ex As Exception
                        xDOKKD = ""
                    End Try

                    Try
                        xDOKNO = ReplStr(OleReader.GetString(2))
                    Catch ex As Exception
                        xDOKNO = ""
                    End Try

                    Try
                        xDOKTG = OleReader.GetDateTime(3)
                    Catch ex As Exception
                        xDOKTG = Nothing
                    End Try
                    SQLstr = "" & _
                        "insert into pib_tblPibDokall (" & _
                        "   CAR, DOKKD, DOKNO, DOKTG" & _
                        ") VALUES (" & _
                        "   '" & xCAR & "', '" & xDOKKD & "', '" & xDOKNO & "', '" & Format(xDOKTG, "yyyy-MM-dd hh:mm:ss") & "'" & _
                        ")"
                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                    If affrow < 0 Then
                        MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                        Exit Sub
                    End If
                End While
            End If
            OleReader.Close()




            yCAR = ""
            SQLstr = "" & _
               " select TBLPIBRES.DOKRESNO, TBLPIBRES.DOKRESTG, TBLPIBRES.RESKD, TBLPIBRES.RESTG, TBLPIBRES.RESWK, TBLPIBRES.KPBC, TBLPIBRES.DESKRIPSI, TBLPIBRES.CAR " & _
               " from TBLPIBRES" & _
               " where  format(TBLPIBRES.dokrestg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' " & _
               " order by TBLPIBRES.CAR asc"
            OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
            If Not OleReaderA Is Nothing Then
                While OleReaderA.Read()
                    Try
                        xDOKRESNO = ReplStr(OleReaderA.GetString(0))
                    Catch ex As Exception
                        xDOKRESNO = ""
                    End Try

                    Try
                        xDOKRESTG = OleReaderA.GetDateTime(1)
                    Catch ex As Exception
                        xDOKRESTG = Nothing
                    End Try

                    Try
                        xPIBNO = ReplStr(OleReaderA.GetString(2))
                    Catch ex As Exception
                        xPIBNO = ""
                    End Try

                    Try
                        xRESTG = OleReaderA.GetDateTime(3)
                    Catch ex As Exception
                        xRESTG = Nothing
                    End Try

                    Try
                        xRESWK = ReplStr(OleReaderA.GetString(4))
                    Catch ex As Exception
                        xRESWK = ""
                    End Try

                    Try
                        xKPBC = ReplStr(OleReaderA.GetString(5))
                    Catch ex As Exception
                        xKPBC = ""
                    End Try

                    Try
                        xDESKRIPSI = ReplStr(OleReaderA.GetString(6))
                    Catch ex As Exception
                        xDESKRIPSI = ""
                    End Try

                    Try
                        xCAR = ReplStr(OleReaderA.GetString(7))
                    Catch ex As Exception
                        xCAR = ""
                    End Try

                    If xRESWK <> "" Then
                        If yCAR <> xCAR Then
                            yCAR = xCAR
                            SQLstr = "delete from PIB_TBLPIBRES where CAR = '" & yCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        End If


                        SQLstr = "" & _
                            " insert into PIB_TBLPIBRES (" & _
                            "    DOKRESNO, DOKRESTG, RESKD, CAR, RESTG, RESWK, KPBC, DESKRIPSI" & _
                            " ) VALUES (" & _
                            "    '" & xDOKRESNO & "', '" & Format(xDOKRESTG, "yyyy-MM-dd hh:mm:ss") & "', '" & xRESKD & "', '" & xCAR & "', '" & Format(xRESTG, "yyyy-MM-dd hh:mm:ss") & "', '" & xRESWK & "', '" & xKPBC & "', '" & xDESKRIPSI & "'" & _
                            " )"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        If affrow < 0 Then
                            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                            Exit Sub
                        End If
                    End If
                End While
            End If
            OleReaderA.Close()

            yCAR = ""

            OleReaderA = Nothing
            SQLstr = "" & _
                " select TBLPIBHDR.PIBNO, TBLPIBHDR.PIBTG, TBLPIBHDR.CAR " & _
                " from TBLPIBHDR, tblpibres " & _
                " where TBLPIBHDR.CAR = tblpibres.car and format(tblpibres.dokrestg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' " & _
                " order by TBLPIBHDR.CAR asc"
            OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
            If Not OleReaderA Is Nothing Then
                While OleReaderA.Read()
                    Try
                        xPIBNO = ReplStr(OleReaderA.GetString(0))
                    Catch ex As Exception
                        xPIBNO = ""
                    End Try
                    Try
                        xPIBTG = OleReaderA.GetDateTime(1)
                    Catch ex As Exception
                        xPIBTG = Nothing
                    End Try

                    Try
                        xCAR = ReplStr(OleReaderA.GetString(2))
                    Catch ex As Exception
                        xCAR = ""
                    End Try

                    If xPIBNO <> "" Then
                        If yCAR <> xCAR Then
                            yCAR = xCAR
                            SQLstr = "delete from pib_TBLPIBHDR where CAR = '" & yCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        End If

                        SQLstr = "" & _
                                "insert into pib_TBLPIBHDR (" & _
                                "   CAR, PIBNO, PIBTG" & _
                                ") VALUES (" & _
                                "   '" & xCAR & "', '" & xPIBNO & "', '" & Format(xPIBTG, "yyyy-MM-dd hh:mm:ss") & "'" & _
                                ")"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        If affrow < 0 Then
                            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                            Exit Sub
                        End If
                    End If
                End While
            End If
            OleReaderA.Close()




            yCAR = ""
            strsql = " SELECT tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK,  tblPibRes.KPBC, tblTabel.URAIAN, tblPibRes.DesKripsi, tblPibRes.car " & _
                     " FROM tblPibRes, tblTabel " & _
                     " Where tblPibRes.RESKD = tblTabel.KDREC " & _
                     "      And format(tblPibRes.dokrestg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' " & _
                     " ORDER BY tblPibRes.CAR desc "

            OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
            iLoop = 1
            If Not OleReader Is Nothing Then
                While OleReader.Read()
                    Try
                        xRESKD = ReplStr(OleReader.GetString(0))
                        xRESTG = OleReader.GetDateTime(1)
                        xRESWK = OleReader.GetString(2)
                        xKPBC = ReplStr(OleReader.GetString(3))
                        xRESKRIP = ReplStr(Trim(OleReader.GetString(4)))
                        If xRESKRIP <> "" Then xRESKRIP = "[NOTES]: " & xRESKRIP & ""
                        xRESDES = ReplStr(Trim(OleReader.GetString(5))) & " " & xRESKRIP
                        xCAR = ReplStr(Trim(OleReader.GetString(6)))

                    Catch ex As Exception
                        xRESKD = ""
                        xRESTG = Nothing
                        xRESWK = ""
                        xKPBC = ""
                        xRESKRIP = ""
                        xRESDES = ""
                        xCAR = ""
                    End Try




                    If xRESTG <> Nothing Then
                        If yCAR <> xCAR Then
                            yCAR = xCAR
                            SQLstr = "delete from tbl_pib_history where aju_no = '" & yCAR & "'"
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        End If
                        If xRESWK <> "" Then
                            xRESWK = Mid(xRESWK, 1, 2) & ":" & Mid(xRESWK, 3, 2) & ":" & Mid(xRESWK, 5, 2)
                        Else
                            xRESWK = "00:00:00"
                        End If

                        xRESTG = xRESTG & " " & xRESWK

                        SQLstr = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                 & " values ('" & xCAR & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "','" & Format(xRESTG, "yyyy-MM-dd hh:mm:ss") & "')"

                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                    End If
                    iLoop = iLoop + 1
                End While
            End If
            OleReaderA.Close()

            yCAR = ""
            OleReaderA = Nothing
            SQLstr = "" & _
                " select TBLPIBCON.CONTNO, TBLPIBCON.CONTUKUR, TBLPIBCON.CONTTIPE, TBLPIBCON.CAR " & _
                " from TBLPIBCON, tblpibres " & _
                " where TBLPIBCON.CAR = tblpibres.car and format(tblpibres.dokrestg,'yyyy-mm-dd') >= '" & Format(dt1.Value, "yyyy-MM-dd") & "' " & _
                " order by TBLPIBCON.CAR asc"
            OleReaderA = DBQueryOleReader(SQLstr, OleConn, "", FileNm)
            If Not OleReaderA Is Nothing Then
                While OleReaderA.Read()
                    Try
                        xCONTNO = ReplStr(OleReaderA.GetString(0))
                    Catch ex As Exception
                        xCONTNO = ""
                    End Try
                    Try
                        xCONTUKUR = ReplStr(OleReaderA.GetString(1))
                    Catch ex As Exception
                        xCONTUKUR = ""
                    End Try
                    Try
                        xCONTTIPE = ReplStr(OleReaderA.GetString(2))
                    Catch ex As Exception
                        xCONTTIPE = ""
                    End Try

                    Try
                        xCAR = ReplStr(OleReaderA.GetString(3))
                    Catch ex As Exception
                        xCAR = ""
                    End Try

                    If yCAR <> xCAR Then
                        yCAR = xCAR
                        SQLstr = "delete from PIB_TBLPIBCON where CAR = '" & yCAR & "'"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                    End If
                    If xCONTNO <> "" Then
                        SQLstr = "" & _
                                "insert into PIB_TBLPIBCON (" & _
                                "   CONTNO, CONTUKUR, CONTTIPE, CAR" & _
                                ") VALUES (" & _
                                "   '" & xCONTNO & "', '" & xCONTUKUR & "', '" & xCONTTIPE & "', '" & xCAR & "'" & _
                                ")"
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        If affrow < 0 Then
                            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                            Exit Sub
                        End If
                    End If
                End While
            End If
            OleReaderA.Close()



        End If
        OleReader.Close()


        SQLstr = "update pib_tblPibDok set CDOKNO = REPLACE(DOKNO,' ','') where CDOKNO is null "
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

        SQLstr = "update pib_tblPibDokall set CDOKNO = REPLACE(DOKNO,' ','') where CDOKNO is null "
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)



    End Sub

    Public Function DataOK(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return False
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Return True
    End Function

    Sub New()
        mac1 = GetUserMACAddress()

        InitializeComponent()
        btnDelete.Enabled = False
        dt1.Checked = True
        dt1.Value = DateAdd(DateInterval.Day, -15, Now())
    End Sub

    Function GetUserMACAddress() As String
        Dim strQuery As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"
        Dim query As ManagementObjectSearcher = New ManagementObjectSearcher(strQuery)
        Dim queryCollection As ManagementObjectCollection = query.Get()
        Dim mo As ManagementObject

        GetUserMACAddress = ""
        For Each mo In queryCollection
            GetUserMACAddress = mo("MacAddress").ToString()
            GetUserMACAddress = Regex.Replace(GetUserMACAddress, ":", "-")
            Exit For
        Next
    End Function

    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select folder_name as DestinationFile from tbr_folderpib where trim(mac_address)=trim('" & mac1 & "')) as a")
        DataGridView1.Columns(0).Width = 400

        btnProcess.Enabled = False
        If dt1.Checked = True And DataGridView1.RowCount > 0 Then btnProcess.Enabled = True

        btnDelete.Enabled = False
        btnSave.Enabled = False
        btnFile.Enabled = True
        txtFolder_Name.Clear()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbr_folderpib " & _
                 "where trim(folder_name)=trim('" & EscapeStr(txtFolder_Name.Text) & "') and trim(mac_address)=trim('" & mac1 & "')"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        teks = "Save Data"

        If baru Then
            SQLstr = "Select * from tbr_folderpib where folder_name='" & EscapeStr(txtFolder_Name.Text) & "' and trim(mac_address)=trim('" & mac1 & "')"
            If DataOK(SQLstr) = False Then
                MsgBox("Folder Data already created! ", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If

            ErrMsg = "Failed when saving Data"
            SQLstr = "INSERT INTO tbr_folderpib (folder_name, mac_address) " & _
                     "VALUES ('" & EscapeStr(txtFolder_Name.Text) & "','" & mac1 & "')"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
        End If
    End Sub

    Private Sub txtFolder_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolder_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtFolder_Name.Text = DataGridView1.Item(0, brs).Value.ToString
        btnDelete.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)
        btnSave.Enabled = False
        btnFile.Enabled = False
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbr_folderpib where folder_name = '" & txtFolder_Name.Text & "' and trim(mac_address)=trim('" & mac1 & "')"
        ErrMsg = "Failed when read File Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtFolder_Name.Text = ""

            While MyReader.Read
                Try
                    txtFolder_Name.Text = MyReader.GetString("folder_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                btnFile.Enabled = True
            Else
                baru = False
                edit = True
                txtFolder_Name.Enabled = False
                btnFile.Enabled = False
            End If
            btnDelete.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)

            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    

    

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        'For i = 0 To DataGridView1.RowCount - 1
        '    Call ProcessFile(DataGridView1.Item(0, i).Value.ToString)
        'Next
        Call ProcessFile(txtFolder_Name.Text)

        MsgBox("End Process ...", MsgBoxStyle.Information, v_idtable)
    End Sub

    Friend Function CekOleConn(ByVal FileName As String, ByVal OleConn As OleDbConnection) As OleDbConnection
        If OleConn Is Nothing Then
            Try
                OleConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & "; Jet OLEDB:Database Password=MumtazFarisHana;")
                OleConn.Open()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
            End Try
        Else
            If OleConn.State = ConnectionState.Closed Then OleConn.Open()
        End If
        Return OleConn
    End Function

    Friend Sub CloseOleConn(ByVal OleConn As OleDbConnection)
        Try
            If Not OleConn Is Nothing Then
                If OleConn.State = ConnectionState.Open Then
                    OleConn.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
        End Try
    End Sub

    Friend Function DBQueryOleReader(ByVal SQLStr As String, ByVal OleConn As OleDbConnection, _
                                    ByVal ErrMsg As String, ByVal FileName As String) As OleDbDataReader

        OleConn = CekOleConn(FileName, OleConn)
        If OleConn Is Nothing Then Return Nothing

        Dim OleCmd As New OleDbCommand(SQLStr, OleConn)
        Try
            Dim OleReader As OleDbDataReader = OleCmd.ExecuteReader()
            Return OleReader
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "SQL error " & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")

            Return Nothing
        End Try
    End Function

    Private Sub dt1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dt1.ValueChanged
        btnProcess.Enabled = False
        If dt1.Checked = True And DataGridView1.RowCount > 0 Then btnProcess.Enabled = True
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim opendialog As New OpenFileDialog

        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFolder_Name.Text = opendialog.FileName
        End If
    End Sub

    Private Function ReplStr(ByVal Rstr As String) As String
        Dim temp As String

        temp = Replace(Rstr, "\", "/")
        Return temp
    End Function


    Private Sub ProcessFile1(ByVal FileName As String)
        Dim OleConn As OleDbConnection
        Dim OleReader As OleDbDataReader
        Dim RstQA, RstQB As DataTableReader

        Dim strsql As String
        Dim BCAJU, BCPIB, BCSPPB As String
        Dim xShipNo, xBL, xHOSTBL, xSUP, xSUPNM, xAJU, xPIB, xSPPB, xVessel, xDELDT As String
        Dim xRESKD, xKPBC, xRESKRIP, xRESDES As String
        Dim xContNo, xContTipe, xCont, xContUnit As String
        Dim DCKD, DCNO As String
        Dim BCPIB_DT, BCSPPB_DT, xRESTG, DCDT As Date
        Dim iLoop, irec, xContTot, iChk As Integer
        Dim xRESWK, KeyBL As String

        ' Error Handling Variables
        Dim strTmp As String

        Dim FileNm As String
        FileNm = FileName

        OleConn = CekOleConn(FileNm, OleConn)

        SQLstr = "Select t1.shipment_no, t1.bl_no, t1.received_copydoc_dt, t1.supplier_code, m2.supplier_name, " _
         & "if(t1.aju_no is null, '', t1.aju_no ) aju_no, if(t1.pib_no is null, '', t1.pib_no) pib_no, if(t1.sppb_no is null, '', t1.sppb_no) sppb_no, t1.hostbl, " _
         & "trim(t1.vessel) vessel, IF(t1.est_delivery_dt IS NULL, '', CAST(DATE_FORMAT(t1.est_delivery_dt,'%Y-%m-%d') AS CHAR)) est_delivery_dt " _
         & "From tbl_shipping t1, tbm_supplier m2 " _
         & "Where t1.supplier_code=m2.supplier_code " _
         & "and received_copydoc_dt >= '" & Format(dt1.Value, "yyyy-MM-dd") & "'"

        ErrMsg = "Failed when read data"
        ErrMsg2 = "Failed when update data"
        RstQA = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not RstQA Is Nothing Then
            While RstQA.Read

                xShipNo = RstQA.GetInt32(0)
                xBL = RstQA.GetString(1)
                xSUP = RstQA.GetString(3)
                xSUPNM = RstQA.GetString(4)
                xAJU = RstQA.GetString(5)
                xPIB = RstQA.GetString(6)
                xSPPB = RstQA.GetString(7)
                xHOSTBL = RstQA.GetString(8)
                xVessel = RstQA.GetString(9)
                xDELDT = RstQA.GetString(10)

                'If xSPPB = "" Then
                '---Update AJU---
                BCAJU = ""
                KeyBL = "MASTER"
                iChk = 0

                OleReader = DBQueryOleReader("SELECT tblPibDok.CAR From tblPibDok " & _
                                             "Where (tblPibDok.DokKd = '705' or tblPibDok.DokKd = '704') And (tblPibDok.DokNo= '" & xBL & "')", OleConn, "", FileNm)

                If Not OleReader Is Nothing Then
                    While OleReader.Read()
                        iChk = iChk + 1
                        BCAJU = ReplStr(OleReader.GetString(0))
                    End While
                End If
                OleReader.Close()

                'untuk kapal curah lakukan filter lagi karena ada kemungkinan nomor BL sama untuk beberapa pengiriman.
                If iChk > 1 Then
                    OleReader = DBQueryOleReader("SELECT tblPibDok.CAR From tblPibDok " & _
                                             "Where (tblPibDok.DokKd = '705' or tblPibDok.DokKd = '704') And (tblPibDok.DokNo= '" & xBL & "') And (format(tblPibDok.DokTg,'yyyy-mm-dd')= '" & xDELDT & "')", OleConn, "", FileNm)

                    If Not OleReader Is Nothing Then
                        If OleReader.HasRows Then
                            OleReader.Read()
                            BCAJU = ReplStr(OleReader.GetString(0))
                        End If
                    End If
                    OleReader.Close()
                End If

                '---handle dari HOSTBL
                If BCAJU = "" Then
                    iChk = 0

                    OleReader = DBQueryOleReader("SELECT tblPibDok.CAR From tblPibDok " & _
                                             "Where (tblPibDok.DokKd = '705' or tblPibDok.DokKd = '704') And (tblPibDok.DokNo= '" & xHOSTBL & "')", OleConn, "", FileNm)

                    If Not OleReader Is Nothing Then
                        While OleReader.Read()
                            iChk = iChk + 1
                            BCAJU = ReplStr(OleReader.GetString(0))
                            KeyBL = "HOST"
                        End While
                    End If
                    OleReader.Close()

                    'untuk kapal curah lakukan filter lagi karena ada kemungkinan nomor BL sama untuk beberapa pengiriman.
                    If iChk > 1 Then
                        OleReader = DBQueryOleReader("SELECT tblPibDok.CAR From tblPibDok " & _
                                                 "Where (tblPibDok.DokKd = '705' or tblPibDok.DokKd = '704') And (tblPibDok.DokNo= '" & xHOSTBL & "') And (format(tblPibDok.DokTg,'yyyy-mm-dd')= '" & xDELDT & "')", OleConn, "", FileNm)

                        If Not OleReader Is Nothing Then
                            If OleReader.HasRows Then
                                OleReader.Read()
                                BCAJU = ReplStr(OleReader.GetString(0))
                            End If
                        End If
                        OleReader.Close()
                    End If
                End If

                If BCAJU <> "" Then

                    If KeyBL = "MASTER" Then
                        SQLstr = "Update tbl_shipping set aju_no='" & BCAJU & "' " _
                                & "where bl_no='" & xBL & "' and supplier_code = '" & xSUP & "'"
                    Else
                        SQLstr = "Update tbl_shipping set aju_no='" & BCAJU & "' " _
                                & "where hostbl='" & xHOSTBL & "' and supplier_code = '" & xSUP & "'"
                    End If
                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                    '---Update PIB---
                    BCPIB = ""
                    BCPIB_DT = Nothing
                    OleReader = DBQueryOleReader("SELECT tblPibHdr.PibNo, tblPibHdr.PibTg FROM tblPibHdr " & _
                                                 "Where tblPibHdr.CAR = '" & BCAJU & "'", OleConn, "", FileNm)

                    If Not OleReader Is Nothing Then
                        If OleReader.HasRows Then
                            OleReader.Read()
                            Try
                                BCPIB = ReplStr(OleReader.GetString(0))
                                BCPIB_DT = OleReader.GetDateTime(1)
                            Catch ex As Exception
                                BCPIB = ""
                                BCPIB_DT = Nothing
                            End Try
                        End If
                    End If
                    OleReader.Close()

                    If BCPIB_DT <> Nothing Then
                        If KeyBL = "MASTER" Then
                            SQLstr = "Update tbl_shipping set pib_no='" & BCPIB & "', pib_dt = '" & Format(BCPIB_DT, "yyyy-MM-dd") & "' " _
                                     & "where bl_no='" & xBL & "'"
                        Else
                            SQLstr = "Update tbl_shipping set pib_no='" & BCPIB & "', pib_dt = '" & Format(BCPIB_DT, "yyyy-MM-dd") & "' " _
                                     & "where hostbl='" & xHOSTBL & "'"
                        End If
                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                        '---Update SPPB ---
                        BCSPPB = ""
                        BCSPPB_DT = Nothing
                        OleReader = DBQueryOleReader("SELECT tblPibRes.DOKRESNO, tblPibRes.DOKRESTG From tblPibRes " & _
                                                     "WHERE tblPibRes.RESKD='300' And tblPibRes.CAR = '" & BCAJU & "'", OleConn, "", FileNm)

                        If Not OleReader Is Nothing Then
                            If OleReader.HasRows Then
                                OleReader.Read()
                                Try
                                    BCSPPB = ReplStr(OleReader.GetString(0))
                                    BCSPPB_DT = OleReader.GetDateTime(1)
                                Catch ex As Exception
                                    BCSPPB = ""
                                    BCSPPB_DT = Nothing
                                End Try
                            End If
                        End If
                        OleReader.Close()

                        If BCSPPB_DT <> Nothing Then
                            If KeyBL = "MASTER" Then
                                SQLstr = "Update tbl_shipping set sppb_no='" & BCSPPB & "', sppb_dt = '" & Format(BCSPPB_DT, "yyyy-MM-dd") & "' " _
                                         & "where bl_no='" & xBL & "'"
                            Else
                                SQLstr = "Update tbl_shipping set sppb_no='" & BCSPPB & "', sppb_dt = '" & Format(BCSPPB_DT, "yyyy-MM-dd") & "' " _
                                         & "where hostbl='" & xHOSTBL & "'"
                            End If
                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        End If
                    End If

                    '---Update Respon History---
                    If xAJU = "" Then xAJU = BCAJU

                    If xAJU <> "" Then
                        irec = 0
                        SQLstr = "Select if(Max(ord_no) is null,0, Max(ord_no)) ord_no from tbl_pib_history " _
                               & "where aju_no='" & xAJU & "'"

                        RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                        If Not RstQB Is Nothing Then
                            While RstQB.Read
                                Try
                                    irec = RstQB.GetValue(0)
                                Catch ex As Exception
                                    irec = 0
                                End Try
                            End While
                        End If

                        strsql = "SELECT tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK,  tblPibRes.KPBC, tblTabel.URAIAN, tblPibRes.DesKripsi " & _
                                 "FROM tblPibRes, tblTabel Where tblPibRes.RESKD = tblTabel.KDREC " & _
                                 "And tblPibRes.CAR = '" & xAJU & "' " & _
                                 "ORDER BY tblPibRes.CAR, tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK "

                        OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                        iLoop = 1
                        If Not OleReader Is Nothing Then
                            While OleReader.Read()
                                If iLoop > irec Then
                                    Try
                                        xRESKD = ReplStr(OleReader.GetString(0))
                                        xRESTG = OleReader.GetDateTime(1)
                                        xRESWK = OleReader.GetString(2)
                                        xKPBC = ReplStr(OleReader.GetString(3))
                                        xRESKRIP = ReplStr(Trim(OleReader.GetString(4)))
                                        If xRESKRIP <> "" Then xRESKRIP = "[NOTES]: " & xRESKRIP & ""
                                        xRESDES = ReplStr(Trim(OleReader.GetString(5))) & " " & xRESKRIP

                                    Catch ex As Exception
                                        xRESKD = ""
                                        xRESTG = Nothing
                                        xRESWK = ""
                                        xKPBC = ""
                                        xRESKRIP = ""
                                        xRESDES = ""
                                    End Try
                                    If xRESTG <> Nothing Then
                                        If xRESWK <> "" Then
                                            xRESWK = Mid(xRESWK, 1, 2) & ":" & Mid(xRESWK, 3, 2) & ":" & Mid(xRESWK, 5, 2)
                                        Else
                                            xRESWK = "00:00:00"
                                        End If

                                        xRESTG = xRESTG & " " & xRESWK

                                        SQLstr = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                                 & " values ('" & xAJU & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "','" & Format(xRESTG, "yyyy-MM-dd hh:mm:ss") & "')"

                                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                    End If
                                End If
                                iLoop = iLoop + 1
                            End While
                        End If
                        OleReader.Close()

                        '---Update Container---
                        strsql = "SELECT tblPibCon.ContNo, tblPibCon.ContUkur, tblPibCon.ContTipe " & _
                                 "FROM tblPibCon Where tblPibCon.CAR = '" & xAJU & "' "

                        OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                        iLoop = 1
                        If Not OleReader Is Nothing Then
                            While OleReader.Read()
                                Try
                                    xContNo = ReplStr(OleReader.GetString(0))
                                    xContTipe = OleReader.GetString(1) & "" & OleReader.GetString(2)
                                Catch ex As Exception
                                    xContNo = ""
                                    xContTipe = ""
                                End Try

                                If xContNo <> "" Then

                                    SQLstr = "Select * from tbl_shipping_cont " & _
                                             "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""

                                    RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                    If RstQB.HasRows Then
                                        SQLstr = "update tbl_shipping_cont set container_no='" & xContNo & "', unit_code='" & xContTipe & "' " _
                                               & "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""
                                    Else
                                        SQLstr = "insert into tbl_shipping_cont (shipment_no, ord_no, container_no, unit_code) " _
                                               & "values (" & xShipNo & "," & iLoop & ",'" & xContNo & "','" & xContTipe & "')"
                                    End If

                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                                    iLoop = iLoop + 1
                                End If
                            End While

                            SQLstr = "SELECT SUM(1) as tot, unit_code FROM tbl_shipping_cont " & _
                                     "WHERE shipment_no=" & xShipNo & " GROUP BY unit_code"

                            RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)
                            If Not RstQB Is Nothing Then
                                While RstQB.Read
                                    Try
                                        xContTot = RstQB.GetValue(0)
                                        xContUnit = RstQB.GetString(1)
                                    Catch ex As Exception
                                        xContTot = 0
                                    End Try
                                End While
                                If xContTot > 0 Then xCont = "," & xContTot & " x " & xContUnit
                                If xCont <> "" Then
                                    xCont = Mid(xCont, 2, Len(xCont) - 1)
                                    SQLstr = "Update tbl_shipping set total_container='" & xCont & "' where shipment_no='" & xShipNo & "'"
                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                End If
                            End If
                        End If
                        OleReader.Close()

                        '---Update Supporting Documents---
                        strsql = "SELECT tblPibDok.DokKd, tblPibDok.DokNo, tblPibDok.DokTg " _
                                & "FROM tblPibDok Where tblPibDok.CAR = '" & xAJU & "' "

                        OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                        iLoop = 1
                        If Not OleReader Is Nothing Then
                            While OleReader.Read()
                                Try
                                    DCKD = ReplStr(OleReader.GetString(0))
                                    DCNO = ReplStr(OleReader.GetString(1))
                                    DCDT = OleReader.GetDateTime(2)
                                Catch ex As Exception
                                    DCKD = ""
                                    DCNO = ""
                                    DCDT = Nothing
                                End Try

                                If DCDT <> Nothing Then
                                    SQLstr = "Select doc_code from tbm_document where refer_to='" & DCKD & "' "

                                    RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                    If Not RstQB Is Nothing Then
                                        While RstQB.Read
                                            Try
                                                DCKD = RstQB.GetValue(0)
                                            Catch ex As Exception
                                            End Try
                                        End While
                                    End If

                                    If DCKD <> "" Then
                                        SQLstr = "Select * from tbl_doc_custom " & _
                                                 "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""

                                        RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                        If RstQB.HasRows Then
                                            SQLstr = "update tbl_doc_custom set doc_code='" & DCKD & "', doc_no='" & DCNO & "', doc_dt='" & Format(DCDT, "yyyy-MM-dd") & "', doc_remark='' " & _
                                                     "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""
                                        Else
                                            SQLstr = "Insert into tbl_doc_custom (shipment_no, ord_no, doc_code, doc_no, doc_dt, doc_remark) " & _
                                                     "values (" & xShipNo & "," & iLoop & ",'" & DCKD & "','" & DCNO & "','" & Format(DCDT, "yyyy-MM-dd") & "','')"
                                        End If

                                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                        iLoop = iLoop + 1
                                    End If
                                End If
                            End While
                        End If
                        OleReader.Close()
                    End If
                End If
            End While
        End If

Done:

        Exit Sub
    End Sub

End Class