$(function ()
{
    var $modal = $('#doc-modal-1');

    $('#commerceAdd').on('click', function (e) {

        SetNewModel(0, "", "");
        //使用详情看 http://amazeui.org/javascript/modal

        $modal.modal({
            width: 800,
            height: 650
        });

    });
    $('#Cancel').on('click', function () {
        $modal.modal('close');
    });


    //修改和新增的保存
    $('#formSubmit').click(function () {
        var id = 0;
        id = $(this).attr('date-id');
        var type = $('#CommerceTbody').attr('data-type');
        var newModel = {
            id: 0,
            Name: $('#doc-ipt-text-1').val(),
            Conent: editor.getContent(),
        };
        $.post("../Commerce/UpdateCommerce",
            { "Conent": newModel.Conent, "Name": newModel.Name,  "id": id,"type":type },
            function (result) {
                $modal.modal('close');
                if (result.status == 0) {
                    var message = '添加成功';
                    if (id > 0) {
                        message = '编辑成功';
                    }
                    bfeMsgBox.success("", message);
                    GetItems();
                }
                else {
                    bfeMsgBox.error("", result.msg);
                }
                GetItems();
            });
    });

    //删除
    $('.delCommerce').on('click',(function () {
        var id = $(this).attr('data-id');
        $('#my-confirm-Del').modal({
            relatedTarget: this,
            onConfirm: function (options) {
                $.ajax({
                    url: "../Commerce/Delete",
                    data: { id: id },
                    type: "get",
                    dataType: 'json',
                    success: function (result) {
                        bfeMsgBox.success("", result.msg);
                        GetItems(1, 0);
                    },
                    error: function (er) {
                        bfeMsgBox.error("", result.msg);
                    }
                });
            },
            // closeOnConfirm: false,
            onCancel: function () {

            }
        });
    }));

    $(document).on('click', '.editCommerce',(function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: "../Commerce/GetModel",
            data: { id: id },
            type: "post",
            dataType: 'json',
            success: function (result) {
                SetNewModel(result.id, result.Name, result.Contents);
               
                $modal.modal({
                    width: 800,
                    height: 650
                });
            },
            error: function (er) {
                BackErr(er);
            }
        });
    }));
});

//控制网站是否显示商会章程
///开关监控
$(document).on('click', '.am-switch', function () {
    var $checkbox = $(this).find("input[type='checkbox']");
    var state = $checkbox.is(':checked');
    $(this).css({
        'transition-duration': '0.2s'
    });
    $checkbox.prop("checked", !state);
    if (state) {
        $(this).removeClass('am-active');
        UpdateLook(this, 1);
    } else {
        $(this).addClass('am-active');
        UpdateLook(this, 0);
    }
});

function UpdateLook(e, val)
{
    id = $(e).parent('td').attr('data-id')
    $.ajax({
        url: "../Commerce/UpdateLook",
        data: { id: id, Look: val },
        type: "post",
        dataType: 'json',
        success: function (result) {
            bfeMsgBox.success("", result.msg);
            GetItems();
        },
        error: function (er) {
            bfeMsgBox.error("", result.msg);
            GetItems();
        }
    });
}

function SetNewModel(id,title,content)
{
    $('#newTitle').text("编辑");
    $('#formSubmit').attr('date-id', id);
  
    $('#doc-ipt-text-1').val(title);
    editor.setContent(content);
}

function GetItems()
{
    $('#my-modal-loading').modal();//正在加载...
    Name= $('#searchValue').val();
    $.ajax({
        url: "../Commerce/SearchCommerce",
        data: { index: 1,Name:Name,type:$('#CommerceTbody').attr('data-type')},
        type: "post",
        dataType: 'json',
        success: function (result) {
             $('#my-modal-loading').modal('close');
            var htmlTem = '';
          
            result.date.forEach(function (e) {
                htmlTem += '<tr> <td>'+e.id+'</td>';
                htmlTem += '<td>' + e.Name + '</td>';
                htmlTem += '<td class="am-hide-sm-only">' + e.Contents + '</td>';
                htmlTem += ' <td class="am-hide-sm-only" data-id='+e.id+'><div _switch="" class="am-switch  am-switch-success newDisplay ' + (e.LookBool ? 'am-active' : '') + '"><div class="am-switch-handle"><input type="checkbox"  '+(e.LookBool ? 'checked' : '')+'></div></div></td>';
                htmlTem += '  <td class="am-hide-sm-only">' + e.Creater + '</td>';
                htmlTem += ' <td class="am-hide-sm-only">' + e.UpdateTimeStr + '</td>';
                htmlTem += ' <td>';
                htmlTem += ' <div class="">';
                htmlTem += ' <div class="am-btn-group am-btn-group-xs">';
                htmlTem += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary editCommerce"  data-id="' + e.id + '"><span class="am-icon-pencil-square-o"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></span> 编辑</a>';
                htmlTem += ' <a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only delCommerce"  data-id="' + e.id + '"><span class="am-icon-trash-o"></span> 删除</a>';
                htmlTem += '</div>';
                htmlTem += '</div>';
                htmlTem += '</td>';
                htmlTem += '</tr>'
            });
            $("#CommerceTbody").html('');
            $("#CommerceTbody").html(htmlTem);
            //还差重新初始化分页控件
            $('#rowCount').text(result.rowCount);
        },
        error: function (er) {
             $('#my-modal-loading').modal('close');
            bfeMsgBox.error("", "数据更新失败！");
        }
    });
}


$('#searchName').on('click', function (e) {
    GetItems();
});



