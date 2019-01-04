Imports Microsoft.VisualBasic

Public Class ClsLibroInspeccionModelosHTML

    Public Enum Modelos
        Caratula
        ActaConstatacion
        ActaInfraccion
        RegistroPersonal
        Audiencia
    End Enum

    Public Function GenerarModelo(ByVal Modelo As Object, ByVal OP As Modelos) As String

        Select Case OP

            Case Modelos.Caratula
                Return ModeloCaratula(Modelo)

            Case Modelos.ActaConstatacion
                Return ModeloActaConstatacion(Modelo)

            Case Modelos.ActaInfraccion
                Return ModeloActaInfraccion(Modelo)

            Case Modelos.RegistroPersonal


            Case Modelos.Audiencia

        End Select

    End Function

    Private Function ModeloCaratula(ByVal Modelo As ClsCaratula) As String
        Dim actahtml As String
        actahtml = "<html><head></head>" _
                   & "<body>" _
                   & "<h1 style='color: #5e9ca0; text-align: center;'>LIBRO INSPECCION ON LINE</h1>" _
                   & "<h3 style='color: #2e6c80;'>LIBRO NRO: " & Modelo.numeroActa.Trim & "</h3>" _
                   & "<h3 style='color: #2e6c80;'>RAZON SOCIAL: " & Modelo.razonSocial.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>NOMBRE DE FANTASIA: " & Modelo.razonSocial.Trim & " </h3> " _
                   & "<h3 style='color: #2e6c80;'>CUIT: " & Modelo.Cuit.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>FECHA DE INSCRIPCION: " & Modelo.razonSocial.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>NRO DE MATRICULA: " & Modelo.razonSocial.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>DOMICILIO LEGAL: " & Modelo.domicilioLegal & "</h3> " _
                   & "<p>Por la presente se hace el libro de Inspecci&oacute;n</p> " _
                   & "</body> " _
                   & "</html>"
        Return actahtml
    End Function

    Private Function ModeloActaInfraccion(ByVal Modelo As ClsActaInfraccion) As String
        Dim actahtml As String = ""
        actahtml = "<html><head><title></title></head><body style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;"">" & _
            "<div>" & _
            "<h3 style=""font-family: Arial, Helvetica, sans-serif; text-align:right;"">Numero: <span><b>" & Modelo.numeroActa.ToString.Trim & "</b></span></h3>" & _
            "<h2 style=""font-family: Arial, Helvetica, sans-serif; text-align:center;"">ACTA DE INFRACCION</h2>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">En <b> " & Modelo.Localidad.ToString.Trim & _
            "</b> a los <b> " & _
            Modelo.fechaHoraActa.Day.ToString.ToLower & " (" & clsUtiles.NumeroEnLetras(Modelo.fechaHoraActa.Day).ToString.Trim.ToLower & ")" & _
            "</b> días del mes de <b>" & clsUtiles.mesEnLetra(Modelo.fechaHoraActa.Month) & "</b> de <b>" & clsUtiles.NumeroEnLetras(Modelo.fechaHoraActa.Year).ToString.Trim.ToLower & _
            "</b> siendo las <b>" & Modelo.fechaHoraActa.Hour.ToString & ":" & Modelo.fechaHoraActa.Minute.ToString & _
            " horas</b>, el que suscribe, funcionario del Ministerio de Trabajo comprobó que la firma/persona Empleadora: <b>" & _
            Modelo.razonSocial.ToString.Trim & "</b> denominada <b>" & Modelo.NombreFantasia.ToString.Trim & " </b> C.U.I.T. <b>" & clsUtiles.CuitGuiones(Modelo.Cuit.ToString.Trim) & _
            "</b>, con domicilio de libro de Sueldos en: <b>" & Modelo.domicilioLibroSueldo.ToString.Trim & _
            "</b> y domicilio Legal en: <b>" & Modelo.domicilioLegal.ToString.Trim & _
            "</b> correspondiente a la actividad principal: <b>" & Modelo.actividadPrincipal.ToString.Trim & " </b> " & _
            "</b> Ha incurrido en las siguientes violaciones a la legislación laboral: </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""><b>" & Modelo.Articulos.ToString.ToString.Trim & "</b></p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Cantidad de empleados dependientes: <b>" & Modelo.CantidadDeEmpleados.ToString.Trim & "</b> según carga/declarados en el sistema.- </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Con lo que se dio por finalizado, el acto que previa lectura y ratificación firma el funcionario actuante.- </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Cuando el funcionario del Ministerio verifique la comisión de infracciones o la violación de cualquier norma laboral, " & _
            "redactará un acta de infracción la que servirá de acusación, prueba de cargo y hará fe mientras no se pruebe lo contario, " & _
            "haciendo constar lugar, día y hora en que se verifica; nombre del establecimiento y/o nombre y apellido de su propietario " & _
            "y/o denominación social del presunto infractor, descripción del hecho verificado como infracción refiriéndolo a la norma " & _
            "infringida y firma del funcionario actuante y del supuesto infractor o representado. Si este se negara a firmar se dejara " & _
            "constancia de tal hecho en el acta (art. 11 Ley 8015). </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">En la hipótesis del articulo precedente, el funcionario " & _
            "actuante entregará copia del acta labrada dejando constancia " & _
            "de ello, al supuesto infractor, con lo cual este se tendrá por suficientemente notificado (art. 12 ley 8015).- </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""> El presunto infractor tendrá desde que fue notificado un plazo de cinco (5) días hábiles para presentar su descargo " & _
            "y ofrecer y producir la prueba, ésta a su cargo, bajo apercibimiento de interpretarse su silencio como reconocimiento " & _
            "de los hechos contenidos en el acta, y dictar resolución sin más trámite. La prueba se producirá en un solo acto en " & _
            "oportunidad de comparecer el acusado y hacer el descargo y solo podrá diferirse el procedimiento en caso de no poder " & _
            "recepcionarse toda la testimonial en una sola oportunidad, diferimiento que no podrá extenderse más allá de dos (2) " & _
            "días. El plazo indicado en la primera parte del párrafo precedente se extenderá a diez (10) días hábiles cuando el " & _
            "presunto infractor resida fuera del radio urbano donde se asiente la sede central u otra dependencia descentralizada " & _
            "del Ministerio de Trabajo (art. 14 Ley 8015). </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">La transcripción de los artículos pertinentes de la Ley 8015, sin ser un requisito legal, es a los fines de favorecer " & _
            "el derecho de defensa del presunto infractor. </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Con fecha <b>" & clsUtiles.NumeroEnLetras(Modelo.fechaHoraActa.Day).ToString.ToLower.Trim & "</b> de <b>" & clsUtiles.mesEnLetra(Modelo.fechaHoraActa.Month).ToString.Trim & _
            "</b> del año <b>" & clsUtiles.NumeroEnLetras(Modelo.fechaHoraActa.Year).ToString.ToLower.Trim & "</b> el funcionario que suscribe procede a notificar, " & _
            " citar y emplazar a <b>" & Modelo.razonSocial.ToString.Trim & "</b> para que en el plazo de <b>" & Modelo.Plazo.ToString.Trim & _
            "</b> días hábiles, en <b>" & Modelo.LugarPresentacion.ToString.Trim & "</b> ofrezca y produzca la prueba de descargo correspondiente a la " & _
            "infracción referida en el acta, bajo apercibimiento de darlo por conforme si así no lo hiciere.-</p>" & _
            "</body></html>"
        Return actahtml
    End Function

    Private Function ModeloActaConstatacion(ByVal Modelo As ClsActaConstatacion) As String


        Dim actahtml As String = "" & _
            "<html xmlns=""http://www.w3.org/1999/xhtml""><head><title></title></head><body style=""font-family: Arial, Helvetica, sans-serif; text-align:justify""><div style=""float:Right"">Fecha: " & Modelo.fechaHoraActa & " </div> </br> <div><h3 style=""font-family: Arial, Helvetica, sans-serif; text-align:right;"">Numero: <span><b>" & _
            Modelo.numeroActa.ToString & " </span></b></h3></div>" & _
              "<h2 style=""font-family: Arial, Helvetica, sans-serif; text-align:center;"">ACTA CIRCUNSTANCIADA</h2>" & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""><span><b>" & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Ref. Expte.: _____<span><b>" & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""><span><b>" & _
            Modelo.Localidad.ToString & " " & _
            "</b></span><span><b>" & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">En la ciudad de <span><B>" & _
            Modelo.Localidad.ToString & " " & _
            "</B></span>, Departamento <span><b> " & _
            Modelo.Departamento.ToString & " " & _
            "</b></span> Provincia de Córdoba, siendo las <span> <B> " & _
            " " & Modelo.HoraApertura & " " & _
            "</b></span> hs., el que suscribe Inspector del Ministerio de Trabajo se constituye en el domicilio de la firma: <span><b> " & _
            Modelo.razonSocial.ToString & " " & _
            "</b></span> C.U.I.T. No. <span><b> " & _
            Modelo.Cuit.ToString & " " & _
            "</b></span> " & _
            " denominada <span><b> " & _
            Modelo.NombreFantasia.ToString() & " " & _
            "</b></span> sita en calle domicilio de trabajo <span><b> " & _
            Modelo.domicilioActa.ToString & " " & _
            "</b></span> correspondiente a la actividad principal declarada: <span><b> " & _
            " " & Modelo.actividadPrincipal & " " & _
            "</b></span> CCT aplicado: <span><b> " & _
            " " & Modelo.CCTAplicado & " " & _
            "</b></span> a los efectos de realizar Inspección General.</p> " & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""> En la oportunidad soy atendido por <span><b> " & _
            Modelo.AtendidoPorNombre.ToString & " " & _
            "</b></span> D.N.I. <span><b>" & _
            Modelo.AtendidoPorDocumento.ToString.Trim & " " & _
            "</b></span> en caracter de <span><b>" & _
            Modelo.AtendidoPorCaracter.ToString.Trim & " " & _
            "</b></span> quien enterado de los motivos de la visita y previa identificación y acreditación del caracter invocado, accede " & _
            "y facilita el procedimiento. Se deja constancia que soy acompañado y comparecen en el presente acto los señores: <span><b> " & _
            Modelo.Acompanantes.ToString.Trim & " " & _
            "</b></span>.</p><p style= ""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px; ""> " & Modelo.VisibleRP & " </p>" & _
            "<p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Requerida la documentación laboral, la empleadora: <span><b> " & _
            Modelo.DocumentacionPresentada.ToString.Trim & " " & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Seguidamente y en uso de la palabra los comparecientes por la entidad sindical manifiestan que: <span><b>" & _
            Modelo.ManifiestoSindical.ToString.Trim & " " & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Cedida la palabra a la empleadora manifiesta que: <span><b> " & _
            Modelo.ManifiestoEmpleador.ToString.Trim & " " & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Cantidad de empleados registrados en sistema: <span><b>" & _
            Modelo.CantidadDeEmpleados.ToString.Trim & " " & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Cantidad de empleados verificados Por el inspector: <span><b>" & _
            Modelo.CantidadDeEmpleadosVerificados.ToString.Trim & " " & _
            "</b></span></p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Acto seguido, el funcionario actuante y sin perjuicio de las sanciones que pudieran corresponder INTIMA " & _
            "A LA FIRMA PARA QUE <span><b> " & _
            "</b></span> TENGA, MANTENGA Y ACREDITE por ante éste lugar de trabajo y a los efectos de su " & _
            "verificación y por el tiempo no prescripto a la fecha, la siguiente documentación laboral a saber <span><b> " & _
            "<b>  " & Modelo.DocumentacionAPresentar.Trim & "</b>" & _
            "</b></span> quedando por el presente acto," & _
            "la empleadora, debidamente notificada bajo apercibimiento de lo establecido en las Leyes vigentes (Multa, Compulsa por la Fuerza " & _
            "Pública en caso de incumplimiento, Ley 8015). </p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Se elevan las presentes actuaciones a la superioridad " & _
            "para su conocimiento y posterior efecto. </p><p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""> Con lo que se dio por terminado el acto, el cual previa lectura y " & _
            "ratificación en sus terminos, suscriben los comparecientes de conformidad por ante mi funcionario actuante que" & _
            " certifico en lugar y fecha incialmente indicados, siendo las <span><b>" & _
            Modelo.HoraCierre.ToString.Trim & " " & _
            "</b></span> hs., las partes suscriben copia de la presente DOY FE.- </p><p p style=""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"">Observaciones: <span><b>" & _
            Modelo.Observaciones.ToString.Trim & " " & _
            "</b></span> </p>" & _
            "<p style= ""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;""> " & Modelo.ModificaDomicilio & " </P>" & _
            "<p  style= ""font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px; " & Modelo.VisibleAudiencia & " "">" & Modelo.Audiencia & " </P>" & _
            "</div></body></html>"
        Return actahtml

    End Function



End Class
