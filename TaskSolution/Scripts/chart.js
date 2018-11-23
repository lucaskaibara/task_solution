google.charts.load('current', { packages: ["orgchart"] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    $.ajax({
        url: "/Diagrama/ItensDiagrama",
        type: 'POST',
        dataType: 'json',
        success: function (response) {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'nome');
            data.addColumn('string', 'nomePai');
            data.addColumn('string', 'mensagemAlt');
            var arr = [];
            $.each(response, function (i, item) {
                arr.push([item.nome, item.nomePai, item.mensagemAlt]);
            });
            data.addRows(arr);
            var chart = new google.visualization.OrgChart(document.getElementById('grafico'));
            chart.draw(data, { allowHtml: true });
        }
    });
}