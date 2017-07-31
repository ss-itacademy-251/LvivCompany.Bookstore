$(function () {
    var controlFieldset = $('.controls fieldset:first');
    var counter = controlFieldset.find('input').length / 2 - 1;
    var nameLeftInput, nameRightInput, idLeftInput, idRightInput, index1, index2;
    $(document).on('click', '.btn-add', function (e) {
        e.preventDefault();
        counter = counter + 1;
        nameLeftInput = "Authors[" + counter.toString() + "].FirstName";
        nameRightInput = "Authors[" + counter.toString() + "].LastName";
        idLeftInput = "Authors_" + counter.toString() + "__FirstName";
        idRightInput = "Authors_" + counter.toString() + "__LastName";
        index1 = counter - 2;
        index2 = counter - 1;
        var entryToCopy = $(this).parents('.entry:first');
        var newEntry = $(entryToCopy.clone()).appendTo(controlFieldset);
        newEntry.find('input:first').attr("name", nameLeftInput).attr("id", idLeftInput).val('');
        newEntry.find('input:last').attr("name", nameRightInput).attr("id", idRightInput).val('');
        newEntry.find('.text-danger').text('');
        if (counter == 1) {
            controlFieldset.find('.entry:first .btn-add')
                .removeClass('btn-add').addClass('btn-remove')
                .removeClass('btn-success').addClass('btn-danger')
                .html('<span class="glyphicon glyphicon-minus"></span>');
        }
        else {
            controlFieldset.find('.entry:eq(' + index1 + ') .input-group')
                .attr("class", "notinput-group");
            controlFieldset.find('.entry:eq(' + index1 + ') .input-group-btn')
                .remove('.input-group-btn');
            controlFieldset.find('.entry:eq(' + index2 + ') .btn-add')
                .removeClass('btn-add').addClass('btn-remove')
                .removeClass('btn-success').addClass('btn-danger')
                .html('<span class="glyphicon glyphicon-minus"></span>');
        }
    });
    $(document).on('click', '.btn-remove', function (e) {
        e.preventDefault();
        $(".btn-add").parents('.entry').remove();
        counter = counter - 1;
        var index3 = counter - 1;
        $(".btn-remove").removeClass('btn-remove').addClass('btn-add')
            .removeClass('btn-danger').addClass('btn-success').html('<span class="glyphicon glyphicon-plus"></span>');
        if (index3 >= 0) {
            controlFieldset.find('.entry:eq(' + index3 + ') .notinput-group')
                .attr("class", "input-group");
            controlFieldset.find('.entry:eq(' + index3 + ') .input-group')
                .append($("<span class='input-group-btn'><button class='btn btn-danger btn-remove' type='button'><span class='glyphicon glyphicon-minus'></span></button></span>"));
        }
    });
});