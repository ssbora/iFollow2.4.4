function mainpage() { }

$(document).ready(function () {
    debugger;
        $("#SearchBox").autocomplete({
            source: function(request,response) {
                $.ajax({
                    url: "/Wall/AutoCompleteSearch",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.id, value: item.firstName };
                        }))

                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
})

$(document).ready(function () {
    //we bind only to the rateit controls within the products div
    debugger;
    $('#rating .rateit').bind('rated reset', function (e) {
        var ri = $(this);
      
        //if the use pressed reset, it will get value: 0 (to be compatible with the HTML range control), we could check if e.type == 'reset',
        //and then set the value to  null .
        var value = ri.rateit('value');
        var id = ri.data('ratingid');

        ri.rateit('readonly', true);
 
        $.ajax({
            url: '/Wall/Rate', //your server side script
            data: {id:id, value: value }, //our data
            type: 'POST',
            success: function (data) {
                alert("your data has been submited")
 
            },
            error: function (jxhr, msg, err) {
                $('#response').append('<li style="color:red">' + msg + '</li>');
            }
        });
    });
})

$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                     $('#comm-'+result.id).append(result.message);
                }
            });
        }
        return false;
    });
});