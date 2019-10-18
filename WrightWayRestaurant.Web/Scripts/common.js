

window.alert = function (msg,callbackFn) {
    if (layer) {
        layer.alert(msg, { icon: 1, closeBtn: 0 }, "Message", function (index) {
            layer.close(index);
            if (typeof callbackFn == "function") {
                callbackFn();
            }
        });
    }
}

var loadingbar = false;
function ajaxGet(url, data, successfn, errorfn, asyncC, dataType) {
    var token = $('[name=__RequestVerificationToken]');
    var headers = {};
    if (token) {
        headers["__RequestVerificationToken"] = token.val();
    }
    $.ajax({
        headers: headers,
        type: "get",
        data: data,
        url:  url,
        dataType: dataType == undefined ? 'json' : dataType,
        global: false,
        async: asyncC == undefined ? true : false,
        beforeSend: function (e) {
            if (layer)
                loadingbar = layer.load("loading...");
        },
        complete: function () {
            if (loadingbar)
                layer.close(loadingbar);
        },
        success: function (d) {
            if (typeof successfn === "function") {
                successfn(d);
            }               
        },
        error: function (e) {
            if (typeof errorfn === 'function') {
                errorfn(e);
            }
        }
    });
}

function ajaxPost(url, data, successfn, errorfn, asyncC, dataType) {
    var token = $('[name=__RequestVerificationToken]');
    var headers = {};
    if (token) {
        headers["__RequestVerificationToken"] = token.val();
    }
    $.ajax({
        type: "post",
        data: data,
        headers: headers,
        url: url,
        dataType: dataType == undefined ? 'json' : dataType,
        global: false,
        async: asyncC == undefined ? true : false,
        beforeSend: function (e) {
            if (layer)
                loadingbar = layer.load("loading...");
        },
        complete: function () {
            if (loadingbar)
                layer.close(loadingbar);
        },
        success: function (d) {
            if (typeof successfn === "function") {
                successfn(d);
            }
        },
        error: function (e) {
            if (errorfn === "function") {
                errorfn(e);
            }
        }
    });
}

function fileUpload(file, params,url,successfn, errorfn) {
    params["ts"] = new Date().getTime();
    //var hidden = $("#form_body").find("input[type='hidden']");
    //var idValue = hidden.val();
    var files = [];
    $.each(file, function (i, item) {
        files.push($(item).attr("id"));
    });
      
    $.ajaxFileUpload({
        url: url,
        type: 'Post',
        contentType: "application/json",
        data: params,
        secureuri: false,
        fileElementId: files,
        //dataType: "jsonp",
        beforeSend: function (e) {
            if (layer)
                loadingbar = layer.load("submiting...");
        },
        complete: function () {
            if (loadingbar)
                layer.close(loadingbar);
        },
        success: function (result, status) {
            if (typeof successfn === "function") {
                successfn(result);
            }
        },
        error: function (err, r) {
            if (errorfn === "function") {
                errorfn(err);
            }
        }
    });
}

function serializeForm(form) {
    var $form = $(form);
    var o = {};
    var a = $form.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    //序列化单选款与多选框
    var $radio = $('input[type=radio],input[type=checkbox]', $form);
    $.each($radio, function () {
        if (!o.hasOwnProperty(this.name)) {
            if ($("input[name='" + this.name + "']:checked").length == 0) {
                //未选中则为0
                o[this.name] = "0";
            }
        }
    });
    return o;
}

function getFormData(formId) {
    var $form = $("form[id='" + formId + "']");
    var o = {};
    var result = true;
    $form.find("input,select").each(function (i, $this) {
        $this = $(this);
        var value = $this.val();
        o[$this.attr("name")] = value;
        var tag = $this.prop("tagName");        
        switch (tag) {
            case "INPUT":
                var type = $this.attr("type");
                switch ($this.attr("type").toLowerCase()) {
                    case "text":
                    case "number":
                    case "hidden":
                        if ($this.data("required") && (value == undefined || value == "")) {
                            layer.msg($this.data("required-message"));
                            $this.focus();
                            result = false;
                            return false;
                        }
                        break;
                }
                break;
            case "TEXTAREA":
            case "SELECT":
                if ($this.data("required") && (value == undefined || value == "")) {
                    layer.msg($this.data("required-message"));
                    $this.focus();
                    result = false;
                    return false;
                }
                break;
        }
    });
    if (!result) return false;   
    return o;
}

function clearFormData(formId) {
    var $form = $("form[id='" + formId + "']");
 
    $form.find("input,select").each(function (i, $this) {
        $this = $(this).val("");      
    });
}

function deserializeForm(formId, data) {
    var $form = $("form[id='" + formId + "']");
    for (var name in data) {
        var $element = $form.find("[id='" + name + "']") || $form.find("[name='" + name + "']");
        if ($element !== undefined && $element.length > 0) {
            var tag = $element.prop("tagName");
            var value = data[name];
            switch (tag) {
                case "INPUT":
                    var type = $element.attr("type");
                    switch ($element.attr("type").toLowerCase()) {
                        case "text":
                        case "number":
                        case "hidden":
                            $element.val(value);
                            break;
                        case "checkbox":
                            $element.prop("checked", value || value == 1);
                            break;
                        case "radio":
                            $element.find("[value='" + value + "']").attr("checked", true);
                            break;
                    }
                    break;
                case "TEXTAREA":
                    $element.text(value);
                    break;
                case "SELECT":
                    $element.find("option[value = '" + value + "']").attr("selected", true);
                    break;
                case "IMG":
                    $element.attr("src", "../" + value);
                    break;
            }
        }

    }
}

function initSelect(url, data, selId, textFiled, valueFiled, defaultValue, errorFunc) {
    ajaxGet(url, data, function (result) {
        var $select = $("select[id='" + selId + "']");
        $.each(result.Data, function (index, item) {
            var option = document.createElement("option");
            option.value = item[valueFiled];
            option.innerHTML = item[textFiled];
            if (defaultValue != undefined && defaultValue == item[valueFiled]) {
                option.selected = "selected";
            }
            $select.append(option);
        });
    }, function () {
        if (typeof errorFunc === 'function') {
            errorFunc();
        } 
    });
}

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}