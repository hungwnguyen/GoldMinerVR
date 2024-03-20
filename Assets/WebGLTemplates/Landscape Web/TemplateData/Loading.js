function isPc() {
  var userAgentInfo = navigator.userAgent;
  var Agents = ["Android", "iPhone",
    "SymbianOS", "Windows Phone",
    "iPad", "iPod"];
  var flag = true;
  for (var v = 0; v < Agents.length; v++) {
    if (userAgentInfo.indexOf(Agents[v]) > 0) {
      flag = false;
      break;
    }
  }
  return flag;
};
function useVoice(language) {
  var recognition = new webkitSpeechRecognition();
  recognition.lang = language == null ? window.navigator.language : language;
  recognition.interimResults = true;

  // Thêm sự kiện 'end' để bắt đầu lại việc nhận dạng giọng nói sau khi kết thúc
  recognition.addEventListener('end', function () {
    recognition.start();
  });

  // Bắt đầu quá trình nhận dạng giọng nói
  recognition.start();

  // Lắng nghe sự kiện 'result' để gửi kết quả đến Unity
  recognition.addEventListener('result', function (event) {
    var result = event.results[event.results.length - 1][0].transcript;
    unityInstance.SendMessage("Bridge", "SendToUnity", result);
  });
}


(function () {
  var backgroundImgUrl = "TemplateData/bg.png";
  var loadingImgUrl = "TemplateData/loading.gif";

  var createStyle = function () {
    var lines = [];

    if (!isPc()) {
      lines.push("@media all and (orientation : landscape) {");
      lines.push("    .cocosLoading_div {");
      lines.push("    }");
      lines.push("}");

      lines.push("@media all and (orientation : portrait){");
      lines.push("        .cocosLoading_div {");
      lines.push("        transform:translate(-50vmax, -50vmin) translate(50vmin, 50vmax) rotate(90deg);");
      lines.push("        -ms-transform:translate(-50vmax, -50vmin) translate(50vmin, 50vmax) rotate(90deg);");
      lines.push("        -moz-transform:translate(-50vmax, -50vmin) translate(50vmin, 50vmax) rotate(90deg);");
      lines.push("        -webkit-transform:translate(-50vmax, -50vmin) translate(50vmin, 50vmax) rotate(90deg);");
      lines.push("        -o-transform:translate(-50vmax, -50vmin) translate(50vmin, 50vmax) rotate(90deg);");
      lines.push("        transform-origin:center center;");
      lines.push("        -o-transform:center center;");
      lines.push("        -ms-transform-origin:center center;");
      lines.push("        -moz-transform-origin:center center;");
      lines.push("        -webkit-transform-origin:center center;");
      lines.push("        -o-transform-origin:center center;");
      lines.push("    }");
      lines.push("}");
    }
    else {
      lines.push("    .cocosLoading_div {");
      lines.push("    }");
    }

    lines.push(".cocosLoading_background_img { position:absolute; margin:0px; padding:0px; left:0px; top:0px; width:100%; height:100%; }");
    lines.push(".cocosLoading_loading_table { position:absolute; width:100%; top:70%; }");

    var code = lines.join("\n");
    return code;
  };

  var createDom = function (id) {
    id = id || "cocosLoading";
    var i, item;
    var div = document.createElement("div");
    div.className = "cocosLoading_div";
    div.id = id;
    div.style.position = "absolute";
    div.style.left = "0px";
    div.style.top = "0px";
    div.style.margin = "0px";
    div.style.padding = "0px";
    div.style.zIndex = "1";
    if (!isPc()) {
      div.style.width = "100vmax";
      div.style.height = "100vmin";
    }
    else {
      div.style.width = "100%";
      div.style.height = "100%";
    }

    var backgroundImg = document.createElement("img");
    backgroundImg.className = "cocosLoading_background_img";
    backgroundImg.src = backgroundImgUrl;
    div.appendChild(backgroundImg);

    var table = document.createElement("table");
    table.className = "cocosLoading_loading_table";
    div.appendChild(table);

    var percentText = document.createElement("span");
    percentText.id = "percent";
    percentText.innerHTML = "0%";

    var row2 = table.insertRow(0);
    var cell2 = row2.insertCell();

    cell2.appendChild(percentText);

    var row = table.insertRow(1);
    var cell = row.insertCell();

    if (window.location.href.indexOf("microclient=1") >= 0 && window.location.href.indexOf("mctype=ios") >= 0) {
    }
    else {
      var loadingImg = document.createElement("img");
      loadingImg.src = loadingImgUrl;
      cell.appendChild(loadingImg);
    }

    document.body.appendChild(div);
    return div;
  };

  (function () {
    var style = document.createElement("style");
    style.type = "text/css";
    style.innerHTML = createStyle();
    document.head.appendChild(style);
    var div = createDom();
  })()

})();

function startAudioOnClick() {
  window.AudioContext = window.AudioContext || window.webkitAudioContext;
  var bind = Function.bind;
  var unbind = bind.bind(bind);

  function instantiate(constructor, args) {
    return new (unbind(constructor, null).apply(null, args));
  }

  window.AudioContext = function (AudioContext) {
    return function () {
      var audioContext = instantiate(AudioContext, arguments);
      window.myAudioContext = audioContext;
      console.log('AudioContext has been instantiated!');
      return audioContext;
    }
  }(AudioContext);

  var webAudioEnabled = false;

  function resumeAudio() {
    if (!webAudioEnabled && window.myAudioContext) {
      console.log('SIMMER starting Audio!');
      window.myAudioContext.resume();
      webAudioEnabled = true;
    }
  }

  document.body.addEventListener('click', resumeAudio, true);
  document.addEventListener('keydown', resumeAudio, true);
}



function unityShowBanner(msg, type) {
  var warningBanner = document.querySelector("#unity-warning");
  function updateBannerVisibility() {
    warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
  }
  var div = document.createElement('div');
  div.innerHTML = msg;
  warningBanner.appendChild(div);
  if (type == 'error') div.style = 'background: red; padding: 10px;';
  else {
    if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
    setTimeout(function () {
      warningBanner.removeChild(div);
      updateBannerVisibility();
    }, 5000);
  }
  updateBannerVisibility();
}
// Move to line 8 in file Build/*.framework.js
/*
function _JS_SystemInfo_GetCanvasClientSize(domElementSelector, outWidth, outHeight) {
  var selector = UTF8ToString(domElementSelector);
  var canvas = selector == "#canvas" ? Module["canvas"] : document.querySelector(selector);
  var w = 0
    , h = 0;
  if (canvas) {
    var size = canvas.getBoundingClientRect();
    if (size.height > size.width && !isPc()) {
      w = size.height;
      h = size.width;
    } else {
      w = size.width;
      h = size.height
    }
  }
  HEAPF64[outWidth >> 3] = w;
  HEAPF64[outHeight >> 3] = h
}

function registerTouchEventCallback(target, userData, useCapture, callbackfunc, eventTypeId, eventTypeString, targetThread) {
  if (!JSEvents.touchEvent)
    JSEvents.touchEvent = _malloc(1696);
  target = findEventTarget(target);
  var touchEventHandlerFunc = function (e) {
    var t, touches = {}, et = e.touches;
    for (var i = 0; i < et.length; ++i) {
      t = et[i];
      t.isChanged = t.onTarget = 0;
      touches[t.identifier] = t
    }
    for (var i = 0; i < e.changedTouches.length; ++i) {
      t = e.changedTouches[i];
      t.isChanged = 1;
      touches[t.identifier] = t
    }
    for (var i = 0; i < e.targetTouches.length; ++i) {
      touches[e.targetTouches[i].identifier].onTarget = 1
    }
    var touchEvent = JSEvents.touchEvent;
    var idx = touchEvent >> 2;
    HEAP32[idx + 3] = e.ctrlKey;
    HEAP32[idx + 4] = e.shiftKey;
    HEAP32[idx + 5] = e.altKey;
    HEAP32[idx + 6] = e.metaKey;
    idx += 7;
    var targetRect = getBoundingClientRect(target);
    var numTouches = 0;
    for (var i in touches) {
      var t = touches[i];
      if (window.innerWidth < window.innerHeight) {
        HEAP32[idx + 1] = t.screenY;
        HEAP32[idx + 2] = window.screen.width - t.screenX;
        HEAP32[idx + 3] = t.clientY;
        HEAP32[idx + 4] = targetRect.width - t.clientX;
        HEAP32[idx + 5] = t.pageY;
        HEAP32[idx + 6] = window.innerWidth - t.pageX;
        HEAP32[idx + 9] = t.clientY - targetRect.top;
        HEAP32[idx + 10] = targetRect.right - t.clientX;
      } else {
        HEAP32[idx + 1] = t.screenX;
        HEAP32[idx + 2] = t.screenY;
        HEAP32[idx + 3] = t.clientX;
        HEAP32[idx + 4] = t.clientY;
        HEAP32[idx + 5] = t.pageX;
        HEAP32[idx + 6] = t.pageY;
        HEAP32[idx + 9] = t.clientX - targetRect.left;
        HEAP32[idx + 10] = t.clientY - targetRect.top;
      }

      HEAP32[idx + 0] = t.identifier;
      HEAP32[idx + 7] = t.isChanged;
      HEAP32[idx + 8] = t.onTarget;
      idx += 13;
      if (++numTouches > 31) {
        break
      }
    }
    HEAP32[touchEvent + 8 >> 2] = numTouches;
    if (function (a1, a2, a3) {
      return dynCall_iiii.apply(null, [callbackfunc, a1, a2, a3])
    }(eventTypeId, touchEvent, userData))
      e.preventDefault()
  };
  var eventHandler = {
    target: target,
    allowsDeferredCalls: eventTypeString == "touchstart" || eventTypeString == "touchend",
    eventTypeString: eventTypeString,
    callbackfunc: callbackfunc,
    handlerFunc: touchEventHandlerFunc,
    useCapture: useCapture
  };
  JSEvents.registerOrRemoveHandler(eventHandler)
}
*/