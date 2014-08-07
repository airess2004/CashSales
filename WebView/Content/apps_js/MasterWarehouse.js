﻿$(document).ready(function () {
    var vStatusSaving,//Status Saving data if its new or edit
		vMainGrid,
		vCode;

    function ClearErrorMessage() {
        $('span[class=errormessage]').text('').remove();
    }

    function ReloadGrid() {
        $("#list").setGridParam({ url: base_url + 'MstWarehouse/GetList', postData: { filters: null }, page: 'first' }).trigger("reloadGrid");
    }

    function ClearData() {
        $('#Description').val('').text('').removeClass('errormessage');
        $('#Name').val('').text('').removeClass('errormessage');
        $('#form_btn_save').data('kode', '');

        ClearErrorMessage();
    }

    $("#form_div").dialog('close');
    $("#delete_confirm_div").dialog('close');


    //GRID+++++++++++++++
    $("#list").jqGrid({
        url: base_url + 'MstWarehouse/GetList',
        datatype: "json",
        colNames: ['ID', 'Code', 'Name', 'Description', 'Is Moving Warehouse','Created At', 'Updated At'],
        colModel: [
    			  { name: 'id', index: 'id', width: 80, align: "center" },
                  { name: 'code', index: 'code', width: 80 },
				  { name: 'name', index: 'name', width: 80 },
                  { name: 'description', index: 'description', width: 250 },
                  { name: 'ismoving', index: 'ismoving', width: 60 },
				  { name: 'createdat', index: 'createdat', search: false, width: 100, align: "center", formatter: 'date', formatoptions: { srcformat: 'Y-m-d', newformat: 'm/d/Y' } },
				  { name: 'updateat', index: 'updateat', search: false, width: 100, align: "center", formatter: 'date', formatoptions: { srcformat: 'Y-m-d', newformat: 'm/d/Y' } },
        ],
        page: '1',
        pager: $('#pager'),
        rowNum: 20,
        rowList: [20, 30, 60],
        sortname: 'name',
        viewrecords: true,
        scrollrows: true,
        shrinkToFit: false,
        sortorder: "ASC",
        width: $("#toolbar").width(),
        height: $(window).height() - 200,
        gridComplete:
		  function () {
		      var ids = $(this).jqGrid('getDataIDs');
		      for (var i = 0; i < ids.length; i++) {
		          var cl = ids[i];
		          row = $(this).getRowData(cl).ismoving;
		          if (row == 'true') {
		              row = "YES";
		          } else {
		              row = "NO";
		          }
		          $(this).jqGrid('setRowData', ids[i], { ismoving: row });
		      }
		  }

    });//END GRID
    $("#list").jqGrid('navGrid', '#toolbar_cont', { del: false, add: false, edit: false, search: false })
           .jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

    //TOOL BAR BUTTON
    $('#btn_reload').click(function () {
        ReloadGrid();
    });

    $('#btn_print').click(function () {
        window.open(base_url + 'Print_Forms/Printmstbank.aspx');
    });

    $('#btn_add_new').click(function () {
        ClearData();
        clearForm('#frm');
        vStatusSaving = 0; //add data mode	
        $('#form_div').dialog('open');
    });

    $('#btn_edit').click(function () {
        ClearData();
        clearForm("#frm");
        var id = jQuery("#list").jqGrid('getGridParam', 'selrow');
        if (id) {
            vStatusSaving = 1;//edit data mode
            var ret = jQuery("#list").jqGrid('getRowData', id);
            $("#form_btn_save").data('kode', id);
            $('#id').val(ret.id);
            $('#Code').val(ret.code);
            $('#Name').val(ret.name);
            $('#Description').val(ret.description);
            $('#Moving').val(ret.ismoving);
            $("#form_div").dialog("open");
        } else {
            $.messager.alert('Information', 'Please Select Data...!!', 'info');
        }
    });

    $('#btn_del').click(function () {
        clearForm("#frm");

        var id = jQuery("#list").jqGrid('getGridParam', 'selrow');
        if (id) {
            var ret = jQuery("#list").jqGrid('getRowData', id);
            //if (ret.deletedimg != '') {
            //    $.messager.alert('Warning', 'RECORD HAS BEEN DELETED !', 'warning');
            //    return;
            //}
            $('#delete_confirm_btn_submit').data('Id', ret.id);
            $("#delete_confirm_div").dialog("open");
        } else {
            $.messager.alert('Information', 'Please Select Data...!!', 'info');
        }
    });

    $('#delete_confirm_btn_cancel').click(function () {
        $('#delete_confirm_btn_submit').val('');
        $("#delete_confirm_div").dialog('close');
    });

    $('#delete_confirm_btn_submit').click(function () {

        $.ajax({
            url: base_url + "MstWarehouse/Delete",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Id: $('#delete_confirm_btn_submit').data('Id'),
            }),
            success: function (result) {
                ReloadGrid();
                $("#delete_confirm_div").dialog('close');
            }
        });
    });

    $('#form_btn_cancel').click(function () {
        vStatusSaving = 0;
        clearForm('#frm');
        $("#form_div").dialog('close');
    });

    $("#form_btn_save").click(function () {



        ClearErrorMessage();

        var submitURL = '';
        var id = $("#form_btn_save").data('kode');

        // Update
        if (id != undefined && id != '' && !isNaN(id) && id > 0) {
            submitURL = base_url + 'MstWarehouse/Update';
        }
            // Insert
        else {
            submitURL = base_url + 'MstWarehouse/Insert';
        }
        var x = document.getElementById("Moving").checked;
        $.ajax({
            contentType: "application/json",
            type: 'POST',
            url: submitURL,
            data: JSON.stringify({
                Id: id, Code : $("#Name").val(),
                Name: $("#Name").val(), Description: $("#Description").val(),
                IsMovingWarehouse: x
            }),
            async: false,
            cache: false,
            timeout: 30000,
            error: function () {
                return false;
            },
            success: function (result) {
                if (JSON.stringify(result.model.Errors) != '{}') {
                    //for (var key in result.model.Errors) {
                    //    if (key != null && key != undefined && key != 'Generic') {
                    //        $('input[name=' + key + ']').addClass('errormessage').after('<span class="errormessage">**' + result.model.Errors[key] + '</span>');
                    //        $('textarea[name=' + key + ']').addClass('errormessage').after('<span class="errormessage">**' + result.model.Errors[key] + '</span>');
                    //    }
                    //    else {
                    //        $.messager.alert('Warning', result.model.Errors[key], 'warning');
                    //    }
                    //}
                    var error = '';
                    for (var key in result.model.Errors) {
                        error = error + "<br>" + key + " " + result.model.Errors[key];
                    }
                    $.messager.alert('Warning', error, 'warning');
                }
                else {
                    ReloadGrid();
                }
            }
        });
    });

    function clearForm(form) {

        $(':input', form).each(function () {
            var type = this.type;
            var tag = this.tagName.toLowerCase(); // normalize case
            if (type == 'text' || type == 'password' || tag == 'textarea')
                this.value = "";
            else if (type == 'checkbox' || type == 'radio')
                this.checked = false;
            else if (tag == 'select')
                this.selectedIndex = -1;
        });
    }


}); //END DOCUMENT READY