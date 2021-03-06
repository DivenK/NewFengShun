﻿$(function () {
    var $modal = $('#doc-modal-1');

    $('#FSHistoryAdd').on('click', function (e) {

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
        var newModel = {
            id: 0,
            Name: $('#doc-ipt-text-1').val(),
            Conent: editor.getContent(),
        };
        if (newModel.Name == "")
        {
           bfeMsgBox.jsError("标题不能为空");
           $('#doc-ipt-text-1').focus();
            return;
        }
        if (newModel.Conent == "")
        {
            bfeMsgBox.jsError("内容不能为空");
            editor.target.focus();
            return;
        }
        $.post("../Chamber/UpdateFsHistory",
            { "Content": newModel.Conent, "title": newModel.Name, "id": id },
            function (result) {
                $modal.modal('close');
                if (result.status == 0) {
                    var message = '添加成功';
                    if (id > 0) {
                        message = '编辑成功';
                    }
                    bfeMsgBox.success("", message);
                    GetItems(1);
                }
                else {
                    bfeMsgBox.error(result.msg, result.msg);
                }
                GetItems(1);
            });
    });

    $(document).on('click', '.delFSHistory', (function () {
        var id = $(this).attr('data-id');
        $('#my-confirm-Del').modal({
            relatedTarget: this,
            onConfirm: function (options) {
                $.ajax({
                    url: "../Chamber/DeleteFSHistory",
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



    $(document).on('click', '.editFSHistory', (function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: "../Chamber/GetModel",
            data: { id: id },
            type: "post",
            dataType: 'json',
            success: function (result) {
                SetNewModel(result.id, result.Title, result.Contents);

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

    SetPage();
});
//开关设置
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

function UpdateLook(e, val) {
    id = $(e).parent('td').attr('data-id')
    $.ajax({
        url: "../Chamber/UpdateDisplay",
        data: { id: id, val: val },
        type: "post",
        dataType: 'json',
        success: function (result) {
            if (result.status == 0) {
                bfeMsgBox.success("", result.msg);
                GetItems(1);
            }
            else {
                bfeMsgBox.error("设置失败");
            }
        },
        error: function (er) {
            bfeMsgBox.error("设置失败");
            GetItems(1);
        }
    });
}

function SetNewModel(id, title, content) {
    var option="添加文史";
    if (id > 0)
    {
        option = "编辑文史";
    }
    $('#newTitle').text(option);
    $('#formSubmit').attr('date-id', id);

    $('#doc-ipt-text-1').val(title);
    editor.setContent(content);
}

function GetItems(index) {
    $('#my-modal-loading').modal();//正在加载...
    Name = $('#searchValue').val();
    $.ajax({
        url: "../Chamber/SearchCommerce",
        data: { index: index, Name: Name },
        type: "post",
        dataType: 'json',
        success: function (result) {
            $('#my-modal-loading').modal('close');
            var htmlTem = '';

            result.date.forEach(function (e) {
                htmlTem += '<tr> <td>' + e.Nid + '</td>';
                htmlTem += '<td>' + e.Title + '</td>';
                htmlTem += '<td class="am-hide-sm-only">' + e.Contents + '</td>';
                htmlTem += ' <td class="am-hide-sm-only" data-id=' + e.id + '><div _switch="" class="am-switch  am-switch-success newDisplay ' + (e.DisplayStr ? 'am-active' : '') + '"><div class="am-switch-handle"><input type="checkbox"  ' + (e.LookBool ? 'checked' : '') + '></div></div></td>';
                htmlTem += '  <td class="am-hide-sm-only">' + e.Creater + '</td>';
                htmlTem += ' <td class="am-hide-sm-only">' + e.UpdateTimeStr + '</td>';
                htmlTem += ' <td>';
                htmlTem += ' <div class="">';
                htmlTem += ' <div class="am-btn-group am-btn-group-xs">';
                htmlTem += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary editFSHistory"  data-id="' + e.id + '"><span class="am-icon-pencil-square-o"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></span> 编辑</a>';
                htmlTem += ' <a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only delFSHistory"  data-id="' + e.id + '"><span class="am-icon-trash-o"></span> 删除</a>';
                htmlTem += '</div>';
                htmlTem += '</div>';
                htmlTem += '</td>';
                htmlTem += '</tr>'
            });
            $("#CommerceTbody").html('');
            $("#CommerceTbody").html(htmlTem);
            //还差重新初始化分页控件
            $('#rowCount').text(result.rowCount);
            SetPage();
        },
        error: function (er) {
            $('#my-modal-loading').modal('close');
            bfeMsgBox.error("", "数据更新失败！");
        }
    });
}

$('#searchName').on('click', function (e) {
    GetItems(1);
});

function SetPage() {
    var pageCount = $('#rowCount').attr('data-pageCount');
    if (pageCount > 1) {
        $('#pageDemo').page({
            pages: pageCount,
            first: "首页", //设置false则不显示，默认为false  
            last: "尾页", //设置false则不显示，默认为false      
            prev: '<', //若不显示，设置false即可，默认为上一页
            next: '>', //若不显示，设置false即可，默认为下一页
            groups: 5, //连续显示分页数
            jump: function (context, first) {
                if (!first)//第一次不执行。
                {
                    GetItems(context.option.curr);
                }
            }//这里就是去异步请求方法
        });
    }
}