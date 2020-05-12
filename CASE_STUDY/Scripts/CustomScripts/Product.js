$(document).ready(function () {
  
    $('#ApplyFilter').click(function () {
        if ($('#From').val() != "") {
            if ($('#To').val() == "") {
                alert("Error");
                return;
            }
        }
        if ($('#From').val() == ""  && $('#To').val() != "") {
            alert("Error");
            return;
        }
       
        $.ajax({
            url: "http://localhost:55292/Ajax/Filter",
            data: { From: $('#From').val(), To: $('#To').val(), Quantity: $('#Quantity').val() },
            type: 'POST',
            success: function (data) {             
                $("#showResults").html(data)  
            },
            error: function (textStatus, errorThrown) {
                alert("Error getting the data");
            }
        });
    });
   

});