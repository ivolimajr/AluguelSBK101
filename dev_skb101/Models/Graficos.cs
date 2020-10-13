using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev_skb101.Models
{
    public class Graficos
    {
        public static string GerarGraficoPizza(string titulo, string dados)
        {
            string graf = @"<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>
            <script type='text/javascript'>
                google.charts.load('current', {packages:['corechart']});
                google.charts.setOnLoadCallback(drawChart);
                
                function drawChart()
                {
                    var data = google.visualization.arrayToDataTable([
                                ['', ''],
                                 " + dados + @"
                                ]);
            
                var options = { 
                                title:'" + titulo + @"',
                                is3D: true,
                              };

                var chart = new google.visualization.PieChart(document.getElementById('piechart_" + titulo.Replace(" ", "") + @"'));
                chart.draw(data, options);
                }
            </script>
            <div id='piechart_" + titulo.Replace(" ", "") + @"' style='min-height: 500px;'></div>";
            return graf;
        }
    }
}