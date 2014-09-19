function checkForParams(buttonEl, checkUrl){
  $('#parameterEditor').replaceWith('<div id="parameterEditor">Loading parameters, please wait...</div>');
  $('#parameterCheck').dialog('open');
  jQuery.get(checkUrl, function(data){
      if(data=='NONE'){
        $('#parameterCheck').dialog('close');
        var button = $(buttonEl);
        button.after('<input type="hidden" name="ForceBuild" value="Force" />');
        buttonEl.parentNode.parentNode.submit();
      }else{
        $('#parameterEditor').replaceWith('<div id="parameterEditor">' + data + '</div>');
      }
    });
}

$(function () {

    var $buildStageOutput = $('#build-stage');

    if ($buildStageOutput.length > 0) {

        var eventSource = new EventSource("ViewProjectReport.ashx?view=event-stream");

        eventSource.addEventListener('message', function (e) {
            var i, data, html = $('<table></table>');

            if (e.data) {
                data = e.data.substring(6).split('\n').map(JSON.parse);
                for (i = 0; i < data.length; i++) {
                    html.append('<tr><td>' + data[i].time + '</td><td>' + data[i].data + '</td></tr>');
                }
                $buildStageOutput.html(html);
            }
            else {
                $buildStageOutput.html('');
            }
        }, false);

    }
});