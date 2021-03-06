﻿$(function () {
    //控制新增的弹层
    var $modal = $('#doc-modal-1');

    $('#addNews').on('click', function (e) {

        SetNewModel(0, "", 1, "", "");
        //使用详情看 http://amazeui.org/javascript/modal

        $modal.modal({
            width: 800,
            height: 790
        });

    });
    $('#Cancel').on('click', function () {
        $modal.modal('close');
    });
    //分页js加载
    SetPageHtml();

    //修改和新增的保存
    $('#formSubmit').click(function () {
        var id = 0;
        id = $(this).attr('date-id');
        var newModel = {
            id: 0,
            TypeId: $('#selectType').val(),
            Name: $('#doc-ipt-text-1').val(),
            Conent: editor.getContent(),
            Author: "",
            Creator: "",
            Praise: 0,
            Look: 0,
            display: 0,
            IsComment: false,
            IsDelete: false,
            CreateTime: $("#doc-datetime-text-1").val()
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
        $.post("../New/UpdateNews",
            { "Conent": newModel.Conent, "Name": newModel.Name, "TypeId": newModel.TypeId, "id": id,"CreateTime":newModel.CreateTime },
            function (result) {
                $modal.modal('close');
                if (result.status == 0) {
                    var message = '发布新闻';
                    if (id > 0) {
                        message = '编辑新闻';
                    }
                    bfeMsgBox.success("", message + result.msg);
                }
                else {
                    bfeMsgBox.error("", result.msg);
                }
                AjaxGetList(1, 0);
            });
    });

    $(document).on('click', '.newEdit',(function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: "../New/GetModel",
            data: { id: id },
            type: "post",
            dataType: 'json',
            success: function (result) {
                SetNewModel(result.id, result.Name, result.TypeId, result.Conent, result.CreatTime);
                $modal.modal({
                    width: 900,
                    height: 800
                });
            },
            error: function (er) {
                BackErr(er);
            }
        });
    }));


    $('.newDel').click(function () {
        var id = $(this).attr('data-id');
        $('#my-confirm-Del').modal({
            relatedTarget: this,
            onConfirm: function (options) {
                $.ajax({
                    url: "../New/DelelNews",
                    data: { id: id },
                    type: "get",
                    dataType: 'json',
                    success: function (result) {
                        bfeMsgBox.success("", result.msg);
                        AjaxGetList(1, 0);
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
    });


    //模糊查询标题
    $('#seachNew').on('click', function (e)
    {
        var earchName = $('#searchName').val();
        AjaxGetList(1, 0, earchName);
    });

    var $page;
});

///开关监控
$(document).on('click','.am-switch',function(){
			var $checkbox=$(this).find("input[type='checkbox']");
			var state=$checkbox.is(':checked');
			$(this).css({
				'transition-duration': '0.2s'
			});
			$checkbox.prop("checked",!state);
			if(state){
			    $(this).removeClass('am-active');
			    UpdateNewDisplay(this,1);
			}else{
			    $(this).addClass('am-active');
                  UpdateNewDisplay(this,0);
			}
});

function UpdateNewDisplay(e,val) {
   var id = $(e).parent('td').attr('data-id');
        $.ajax({
            url: "../New/UpdateDispay",
            data: { id: id, display: val },
            type: "post",
            dataType: 'json',
            success: function (result) {
                bfeMsgBox.success("", result.msg);
                 AjaxGetList(1, 0);
            },
            error: function (er) {
                bfeMsgBox.error("", result.msg);
                AjaxGetList(1, 0);
            }
        });
}


//赋值页面上的分页
function SetPageHtml() {
    var pageCount = $("#pageDemo").attr("data-PageCount");
    var pageIndex = $("#pageDemo").attr("data-indexPage");
   $page=$('#pageDemo').page({
        pages: pageCount,
        first: "首页", //设置false则不显示，默认为false  
        last: "尾页", //设置false则不显示，默认为false      
        prev: '<', //若不显示，设置false即可，默认为上一页
        next: '>', //若不显示，设置false即可，默认为下一页
        groups: 5, //连续显示分页数
        jump: function (context, first) {
            if (!first)//第一次不执行。
            {
                AjaxGetList(context.option.curr, 0);
            }
        }//这里就是去异步请求方法
    });
}

//修改弹窗赋值
function SetNewModel(id, title, typeId, content, d) {
    $('#newTitle').text("编辑新闻");
    $('#formSubmit').attr('date-id', id);
    $('#selectType').val(typeId);
    $('#doc-ipt-text-1').val(title);
    $('#doc-datetime-text-1').datepicker('setValue', d);
    editor.setContent(content);
}

//异步获取数据并更新列表
function AjaxGetList(index, typeId, Name,isChang) {
    $('#my-modal-loading').modal();//正在加载...
    typeId = $('#newType').val();
    Name= $('#searchName').val();
    $.ajax({
        url: "../New/GetList",
        data: { index: index, typeId: typeId,Name:Name},
        type: "post",
        dataType: 'json',
        success: function (result) {
             $('#my-modal-loading').modal('close');
            var htmlTem = '';
            SetAllCount(result.page.count, result.page.pageCount);
            result.rows.forEach(function (e) {
                htmlTem += '<tr>';
                htmlTem += ' <td>'+e.Nid+'</td>';
                htmlTem += '<td>' + e.TypeName + '</td>';
                htmlTem += '<td title="'+e.Name +'">' + (e.Name.length>10?e.Name.substring(0,10):e.Name) + '</td>';
                htmlTem += '<td class="am-hide-sm-only">' + e.Conent+ '</td>';
                htmlTem += ' <td class="am-hide-sm-only" data-id='+e.id+'><div _switch="" class="am-switch am-round am-switch-success newDisplay ' + (e.displayBool ? 'am-active' : '') + '"><div class="am-switch-handle"><input type="checkbox"  '+(e.displayBool ? 'checked' : '')+'></div></div></td>';
                htmlTem += '  <td class="am-hide-sm-only">' + e.Author + '</td>';
                htmlTem += ' <td class="am-hide-sm-only">' + e.CreatTimeStr + '</td>';
                htmlTem += ' <td>';
                htmlTem += ' <div class="">';
                htmlTem += ' <div class="am-btn-group am-btn-group-xs">';
                htmlTem += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary newEdit" id="newEdit" data-id="' + e.id + '"><span class="am-icon-pencil-square-o"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></span> 编辑</a>';
                htmlTem += ' <a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only newDel"  data-id="' + e.id + '"><span class="am-icon-trash-o"></span> 删除</a>';
                htmlTem += '</div>';
                htmlTem += '</div>';
                htmlTem += '</td>';
                htmlTem += '</tr>';
            });
            $("#newTbody").html('');
            $("#newTbody").html(htmlTem);
            //还差重新初始化分页控件
            delPage(result.page.index);
           
        },
        error: function (er) {
             $('#my-modal-loading').modal('close');
            bfeMsgBox.error("", "数据更新失败！");
        }
    });
}

function delPage(index)
{
    if (index == 1) {
        //$page.remove();
        $('#pageDemo').html('');
          SetPageHtml();
    }
}
//总行数赋值
function SetAllCount(count, pageCount) {
    $('#new-pageCount').text(count);
    $('#pageDemo').attr("data-pagecount", pageCount);
}

$('#newType').on('change', function ()
{
    AjaxGetList(1, 0, "",1);
})