$(function () {
    var $modal = $('#doc-modal-1');

    $('#commerceAdd').on('click', function (e) {

        SetNewModel(0, "", "", "");
         var height = 880;
        //使用详情看 http://amazeui.org/javascript/modal
        var type = $("#rowCount").attr('data-type');
        if (type != 2) {
            $("#hideName").css("display", "none");
            $("#SortNo").css("display", "none");
            height = 650;
        }
        $modal.modal({
        width: 800,
        height: height
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
            var getSort = $('#doc-ipt-text-5').val();
            var newModel = {
                id: 0,
                Name: $('#doc-ipt-text-1').val(),
                Conent: editor.getContent(),
                Content2: editor2.getContent()
            };
            if (newModel.Name == "") {
                bfeMsgBox.jsError("标题不能为空");
                $('#doc-ipt-text-1').focus();
                return;
            }
            if (newModel.Conent == "") {
                bfeMsgBox.jsError("内容不能为空");
                editor.target.focus();
                return;
            }
            if (newModel.Content2 == "") {
                bfeMsgBox.jsError("内容不能为空");
                editor2.target.focus();
                return;
            }
            var src = $(newModel.Content2).find('img').attr('src');
            var SortNo = 0;
            if (type == 2&&getSort=="") {
                $('#doc-ipt-text-5').focus();
                return;
            }
            if (type == 2) {
                SortNo = getSort;
            }

            $.post("../Commerce/UpdateCommerce",
                { "Conent": newModel.Conent, "Name": newModel.Name, "id": id, "type": type, "imageURL": src,"remark": SortNo },
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
                        bfeMsgBox.error("", result.msg);
                    }
                    GetItems(1);
                });
        });

        //删除
        $('.delCommerce').on('click', (function () {
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

        $(document).on('click', '.editCommerce', (function () {
            var id = $(this).attr('data-id');
            var type = $("#rowCount").attr('data-type');
            var width = 800;
            var height = 880;
            if (type != 2)
            {
                $("#hideName").css("display", "none");
                $("#SortNo").css("display", "none");
                height =650;
            }
            $.ajax({
                url: "../Commerce/GetModel",
                data: { id: id },
                type: "post",
                dataType: 'json',
                success: function (result) {
                    SetNewModel(result.id, result.Name, result.Contents,result.ImageUrl,result.Remark);
                    $modal.modal({
                        width: width,
                        height: height
                    });
                },
                error: function (er) {
                    BackErr(er);
                }
            });
        }));
        SetPage();
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

    function UpdateLook(e, val) {
        var id = $(e).parent('td').attr('data-id');
        $.ajax({
            url: "../Commerce/UpdateLook",
            data: { id: id, Look: val },
            type: "post",
            dataType: 'json',
            success: function (result) {
                bfeMsgBox.success("", result.msg);
                GetItems(1);
            },
            error: function (er) {
                bfeMsgBox.error("", result.msg);
                GetItems(1);
            }
        });
    }

    function SetNewModel(id, title, content,imageUrl,remark) {
        $('#newTitle').text("编辑");
        $('#formSubmit').attr('date-id', id);

        $('#doc-ipt-text-1').val(title);
        $('#doc-ipt-text-5').val(remark);//同级排序的字段
        editor.setContent(content);
        editor2.setContent("<img src='"+imageUrl+"'/>");
    }

    function GetItems(index) {
        $('#my-modal-loading').modal();//正在加载...
        Name = $('#searchValue').val();
        $.ajax({
            url: "../Commerce/SearchCommerce",
            data: { index: index, Name: Name, type: $('#CommerceTbody').attr('data-type') },
            type: "post",
            dataType: 'json',
            success: function (result) {
                $('#my-modal-loading').modal('close');
                var htmlTem = '';

                result.date.forEach(function (e) {
                    htmlTem += '<tr> <td>' + e.Nid + '</td>';
                    htmlTem += '<td>' + e.Name + '</td>';
                    htmlTem += '<td class="am-hide-sm-only">' + e.Contents + '</td>';
                    htmlTem += ' <td class="am-hide-sm-only" data-id=' + e.id + '><div _switch="" class="am-switch  am-switch-success newDisplay ' + (e.LookBool ? 'am-active' : '') + '"><div class="am-switch-handle"><input type="checkbox"  ' + (e.LookBool ? 'checked' : '') + '></div></div></td>';
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


