$(document).on("dblclick", "#ForeachNodo", function () {
 {

        var tmpId = $(this).find('td').eq(0).text();
        var tmpTipo = $(this).find('td').eq(1).text();
        var tmpNombre = $(this).find('td').eq(2).text();

        $.each(ListaDetalle, function (index, value) {
            var Detalles = [];

            $("#foreachNodo").each(function () {
                
                    var tmpDetalle = {
                        "Id": $(this).find("td")[0].innerText,
                        "Tipo": $(this).find("td")[1].innerText,
                        "Nombre": $(this).find("td")[2].innerText,
                        "Valor": $(this).find("td")[3].getElementsByTagName('input')[0].value.replace(',', '')
                    };
                    Detalles.push(tmpDetalle);
            });
            var tmpDetalle = {
                "Id": tmpId,
                "Tipo": tmpTipo,
                "Nombre": tmpNombre,
                    "Valor": 0
                };
                Detalles.push(tmpDetalle);

            value.ListaDetalle = Detalles;

        });

        var TableDetalleHTML = "";
        $.each(ListaAreas, function (index, tmpArea) {

            TableDetalleHTML += "<tr><td colspan=\"10\" class=\"Head_Areas\">" + tmpArea.sArea + "</td></tr>";

            TableDetalleHTML += "<tr>" +
                "    <th></th>" +
                "    <th class=\"text-center\">AGRUP</th>" +
                "    <th class=\"text-center\">CVEGEN</th>" +
                "    <th class=\"text-center\">CVEACA</th>" +
                "    <th class=\"text-center\">DESCRIPCIÓN</th>" +
                "    <th class=\"text-center\">PRE.UNI.</th>" +
                "    <th class=\"text-center\">PRE.VEN.</th>" +
                "    <th class=\"text-center\">CANTIDAD</th>" +
                "    <th class=\"text-center\">IMPORTE</th>" +
                "    <th></th>" +
                "</tr>";

            $.each(tmpArea.ListaDetalle, function (iDetalle, tmpDetalle) {
                TableDetalleHTML += "<tr id=\"Area_" + tmpArea.idArea + "\">" +
                    "    <td class=\"Area_" + tmpArea.idArea + "\">" + tmpDetalle.NumRen + "</td>" +
                    "    <td class=\"text-center\">" + tmpDetalle.AgrupC + "</td>" +
                    "    <td>" + tmpDetalle.CvePro + "</td>" +
                    "    <td class=\"text-center\">" + tmpDetalle.CveAca + "</td>" +
                    "    <td>" + tmpDetalle.DesPro + "</td>" +
                    "    <td>$" + (new Number(tmpDetalle.PreUni)).toLocaleString('en-US') + "</td>" +
                    "    <td class=\"text-right\"><input type=\"text\" id=\"txtPreVen\" name=\"PreVen\" value=\"$" + (new Number(tmpDetalle.PreVen)).toLocaleString('en-US') + "\" class=\"form-control text-center\"></td>" +
                    "    <td><input type=\"text\" id=\"txtCantidad\" name=\"Cantidad\" value=\"" + (new Number(tmpDetalle.Cantid)).toLocaleString('en-US') + "\" class=\"form-control text-center\"></td>" +
                    "    <td class=\"text-right\">$" + ((new Number(tmpDetalle.PreUni)) * (new Number(tmpDetalle.Cantid))).toLocaleString('en-US') + "</td>" +
                    "    <td><button type=\"button\" class=\"btn\"><i class=\"far fa-trash-alt\"></i></button></td>" +
                    "</tr>";

            });

        });

        $('#TableDetalle tbody').empty();
        $('#TableDetalle tbody').append(TableDetalleHTML);

        $("#txtCvePro").val("");
        $("#txtDesPro").val("");
    }
    else {
        $("#ErrorProductos").html("Es necesario seleccionar un área de destino.");
    }
});
