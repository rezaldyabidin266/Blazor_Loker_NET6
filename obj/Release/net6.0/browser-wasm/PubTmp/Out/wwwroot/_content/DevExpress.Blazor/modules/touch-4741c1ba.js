import{c as e,g as o,b as t,a as n}from"./dom-f057d094.js";var r=e((function(e,o){Object.defineProperty(o,"__esModule",{value:!0}),o.TouchUtils=void 0;var r=function(){function e(){}return e.onEventAttachingToDocument=function(o,n){return!t.Browser.MacOSMobilePlatform||!e.isTouchEventName(o)||(e.documentTouchHandlers[o]||(e.documentTouchHandlers[o]=[]),e.documentTouchHandlers[o].push(n),e.documentEventAttachingAllowed)},e.isTouchEventName=function(e){return t.Browser.WebKitTouchUI&&(e.indexOf("touch")>-1||e.indexOf("gesture")>-1)},e.isTouchEvent=function(e){return t.Browser.WebKitTouchUI&&n.isDefined(e.changedTouches)},e.getEventX=function(e){return t.Browser.IE?e.pageX:e.changedTouches[0].pageX},e.getEventY=function(e){return t.Browser.IE?e.pageY:e.changedTouches[0].pageY},e.touchMouseDownEventName=t.Browser.WebKitTouchUI?"touchstart":t.Browser.Edge&&t.Browser.MSTouchUI&&window.PointerEvent?"pointerdown":"mousedown",e.touchMouseUpEventName=t.Browser.WebKitTouchUI?"touchend":t.Browser.Edge&&t.Browser.MSTouchUI&&window.PointerEvent?"pointerup":"mouseup",e.touchMouseMoveEventName=t.Browser.WebKitTouchUI?"touchmove":t.Browser.Edge&&t.Browser.MSTouchUI&&window.PointerEvent?"pointermove":"mousemove",e.msTouchDraggableClassName="dxMSTouchDraggable",e.documentTouchHandlers={},e.documentEventAttachingAllowed=!0,e}();o.TouchUtils=r}));o(r);export{r as t};
