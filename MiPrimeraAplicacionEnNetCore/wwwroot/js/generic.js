window.onload = function () {
    $(document).ready(function () {
        $('#table').DataTable();
    });
}

function mostrarModal(titulo = "Desea guardar los cambios", texto = "Deseas registrar los cambios en la base de datos") {
   return  Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si'
    })
}


function Imprimir() {

    var tCheck = document.getElementById("tCheck").outerHTML;
    var table = "<h1>Reporte a Imprimir</1>"
    table += document.getElementById("table").outerHTML;
    table = table.replace(tCheck, "");
    var pagina = window.document.body;
    var ventana = window.open();
    ventana.document.write(table);
    ventana.print();
    ventana.close();
    window.document.body = pagina;
}

function ExportarExcel() {
    document.getElementById("tipoReporte").value = "Excel"
    var frmReporte = document.getElementById("frmReporte");

    frmReporte.submit();
}

function ExportarWord() {
    document.getElementById("tipoReporte").value = "Word"
    var frmReporte = document.getElementById("frmReporte");
    frmReporte.submit();
}

//function ExportarPDF() {
//    document.getElementById("tipoReporte").value = "PDF"
//    var frmReporte = document.getElementById("frmReporte");
//    frmReporte.submit();
//}

function ExportarPDF() {
    var frm = new FormData();
    var checks = document.getElementsByName("nombrePropiedades");
    var nChecks = checks.length;
    for (var i = 0; i < nChecks; i++) {
        if (checks[i].checked == true) {
            frm.append("nombrePropiedades[]", checks[i].value);
        }
    }

    $.ajax({
        type: "POST",
        url: "Especialidad/exportarPDFDatosDos",
        data: frm,
        contentType: false,
        processData: false,
        success: function (data) {
            var a = document.createElement("a");
            a.href = data;
            a.download = "reporte.pdf";
            a.click();
        }
    })
}

