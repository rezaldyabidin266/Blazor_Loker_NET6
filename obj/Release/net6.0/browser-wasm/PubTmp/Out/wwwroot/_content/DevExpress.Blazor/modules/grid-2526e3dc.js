import{b as e,d as t}from"./dom-f057d094.js";import{e as o}from"./evt-a5b11244.js";import{t as n}from"./touch-4741c1ba.js";import{l,d as r,m as i,n as s,h as c,q as a,e as d,u,r as f,b as h,o as p,f as g,p as m,v as y,w as b,g as S}from"./dom-utils-ab21af24.js";import{d as v,r as x}from"./disposable-d2c2d283.js";import{minColumnWidth as H,ColumnResizeMode as T}from"./column-resize-70046297.js";import{updateScrollbarStyle as w}from"./dx-style-helper-2b754f53.js";import"./_tslib-bfc426b4.js";import"./key-514968ed.js";const E={GroupPanelHead:"gph",ColumnHead:"ch"};function L(e){if(!e.hasAttribute("data-dxdg-draggable-id"))return null;const t=e.getAttribute("data-dxdg-column-id").split("|"),o=!(t.length>2)||"1"===t[2],n=t.length>1?E[t[1]]:E.ColumnHead,l=n===E.ColumnHead?parseInt(t[0]):-1,r=n===E.GroupPanelHead?parseInt(t[0]):-1,i=e.previousElementSibling;return{columnVisibleIndex:l,groupVisibleIndex:r,columnHeadType:n,canBeGrouped:o,needInsertBeforeToo:!i||!L(i),element:e}}function N(e){return"[data-dxdg-draggable-id="+e+"]"}function I(e,t,o){var n,l,r;n=e,l=B(t,"clientX")-o.left,r=B(t,"clientY")-o.top,n.style.transform=["translate(",Math.round(l),"px, ",Math.round(r),"px)"].join("")}function B(e,t){return void 0!==e[t]?e[t]:void 0!==e.touches?e.touches[0][t]:0}function R(t,o,c,a){const d=B(t,"clientX"),u=B(t,"clientY"),f=t.target;if(c){const e=l(t.target,"th");if(e&&d>=e.getBoundingClientRect().right-H)return}let h=!1;const p=function(e){const t=Math.abs(d-B(e,"clientX"))>10,c=Math.abs(u-B(e,"clientY"))>10;return(t||c)&&(h=!0,g(),function(e,t,o,c){const a=N(o),d=l(t,a);if(!d)return;const u=r(d,"dxbs-gridview").getBoundingClientRect(),f=L(d),h=s(),p=i();let g={left:0,top:0};const m=function(e,t){const o=[],n=N(t),l=document.querySelectorAll(n);let r=!1,i=!1;for(let e=0;e<l.length;e++){const t=l[e],n=t.getBoundingClientRect(),s=L(t),c=s.columnVisibleIndex,a=s.groupVisibleIndex,d=s.columnHeadType;d===E.GroupPanelHead?r=!0:d===E.ColumnHead&&(i=!0),s.needInsertBeforeToo&&o.push(new M(t,n.left,n.top,n.bottom,c,a,d,!0,!1)),o.push(new M(t,n.right,n.top,n.bottom,c,a,d,!1,!1))}if(r||i){if(!r){const e=document.querySelector("[data-dxdg-drag-group-panel="+t+"]");if(e){const t=e.getBoundingClientRect();o.push(new M(e,t.left,t.top,t.bottom,-1,0,E.GroupPanelHead,!1,!0))}}if(!i){const e=document.querySelector("[data-dxdg-drag-head-row="+t+"]");if(e){const t=e.getBoundingClientRect();o.push(new M(e,t.right,t.top,t.bottom,-1,-1,E.ColumnHead,!1,!0))}}}return o}(0,o),y=function(e,t){let o=e.cloneNode(!0);const n=e.getBoundingClientRect(),l={left:B(t,"clientX")-n.left,top:B(t,"clientY")-n.top};if("DIV"!==o.tagName){const t=document.createElement("DIV"),l=window.getComputedStyle(e);t.innerHTML=o.innerHTML,t.className="card "+e.className,t.style.width=n.width+"px",t.style.height=n.height+"px",t.style.paddingTop=l.paddingTop,t.style.paddingBottom=l.paddingBottom,t.style.paddingLeft=l.paddingLeft,t.style.paddingRight=l.paddingRight,o=t}else o.style.width=n.width+"px",o.style.height=n.height+"px";o.className=o.className+" dx-dragging-state",document.body.appendChild(o);const r=o.getBoundingClientRect();return{dragElement:o,offsetFromMouse:{left:r.left+l.left,top:r.top+l.top}}}(d,e),b=y.dragElement,S=y.offsetFromMouse;I(b,e,S);let v=!0,x=null;const H=function(e){v&&(b.style.visibility="visible",v=!1);return I(b,e,{left:S.left+g.left,top:S.top+g.top}),x=function(e,t,o,n,l,r){!function(e){const t=N(e),o=document.querySelectorAll("div.dxgv-target-marks"+t);for(let e=0;e<o.length;e++){const t=o[e];t.parentNode.removeChild(t)}}(t);let c=null;const a=[],d=B(n,"clientX"),u=B(n,"clientY");for(let t=0;t<e.length;t++){const n=e[t];if(n.columnHeadType===E.GroupPanelHead&&!o.canBeGrouped)continue;if(n.top+l.top<=u&&u<=n.bottom+l.top){if(n.wholeRowIsRarget){c=n;break}a.push({distance:Math.abs(Math.abs(n.x+l.left)-Math.abs(d)),target:n})}}if(null==c){let e=1e6;for(const t in a)e>a[t].distance&&C(d,o,a[t].target)&&(e=a[t].distance,c=a[t].target)}null==c||V(o,c)||c.x>=r.left&&c.x<=r.right&&function(e,t){const o=document.createElement("DIV"),n=1,l=2*(16+n),r=e.bottom-e.top+2*n-l+"px";o.className="dxgv-target-marks",o.dataset.dxdgDraggableId=t,o.style.top=e.top+(e.docScrollTop-i())+i()-1-n+"px",o.style.height=l+"px",o.style.left=e.x+(e.docScrollLeft-s())+s()+"px",o.innerHTML=["<svg class='dx-image dx-image-rotate-180' role='img' aria-hidden='true'><use href='_content/DevExpress.Blazor/dx-blazor.svg#dx-arrow-up'></use></svg>","<div style='height:",o.style.height,"'></div>","<svg class='dx-image' style='top: ",r,"' role='img' aria-hidden='true'><use href='_content/DevExpress.Blazor/dx-blazor.svg#dx-arrow-up'></use></svg>"].join(""),document.body.appendChild(o),e.mark=o}(c,t);return c}(m,o,f,e,g,u),e.preventDefault(),!1},T=function(){if(x&&!V(f,x)){c.invokeMethodAsync("OnGridColumnHeadDragNDrop",f.columnHeadType===E.GroupPanelHead?f.groupVisibleIndex:f.columnVisibleIndex,f.columnHeadType,x.columnHeadType===E.GroupPanelHead?x.groupVisibleIndex:x.columnVisibleIndex,x.columnHeadType,x.insertBefore),x.mark&&x.mark.parentNode.removeChild(x.mark)}document.removeEventListener(n.TouchUtils.touchMouseMoveEventName,H),document.removeEventListener(n.TouchUtils.touchMouseUpEventName,T),window.removeEventListener("scroll",w),b.parentNode.removeChild(b)},w=function(){g={left:h-s(),top:p-i()}};document.addEventListener(n.TouchUtils.touchMouseMoveEventName,H),document.addEventListener(n.TouchUtils.touchMouseUpEventName,T),window.addEventListener("scroll",w)}(e,f,o,a)),e.preventDefault(),!1},g=function(){document.removeEventListener(n.TouchUtils.touchMouseMoveEventName,p),document.removeEventListener(n.TouchUtils.touchMouseUpEventName,g),!h&&f&&e.Browser.WebKitTouchUI&&f.click()};document.addEventListener(n.TouchUtils.touchMouseMoveEventName,p),document.addEventListener(n.TouchUtils.touchMouseUpEventName,g),t.preventDefault(),f.focus()}function M(e,t,o,n,l,r,c,a,d){this.element=e,this.x=t,this.top=o,this.bottom=n,this.columnVisibleIndex=l,this.groupVisibleIndex=r,this.columnHeadType=c,this.insertBefore=a,this.wholeRowIsRarget=d,this.docScrollTop=i(),this.docScrollLeft=s()}function V(e,t){function o(e,t){return t.groupVisibleIndex===e.groupVisibleIndex||t.groupVisibleIndex===e.groupVisibleIndex-1&&!t.insertBefore}function n(e,t){return t.columnVisibleIndex===e.columnVisibleIndex||t.columnVisibleIndex===e.columnVisibleIndex-1&&!t.insertBefore}if(t.columnHeadType===e.columnHeadType&&e.columnHeadType===E.GroupPanelHead&&o(e,t))return!0;if(t.columnHeadType===e.columnHeadType&&e.columnHeadType===E.ColumnHead&&n(e,t))return!0;if(e.columnHeadType===E.GroupPanelHead&&t.columnHeadType===E.ColumnHead&&-1!==e.columnVisibleIndex&&n(e,t))return!0;return!(e.columnHeadType!==E.ColumnHead||t.columnHeadType!==E.GroupPanelHead||-1===e.groupVisibleIndex||!o(e,t))}function C(e,t,o){const n=t.element.getBoundingClientRect();if(V(t,o)&&(e<n.left||e>n.right))return!1;if(o.x<n.left){if(e>n.right)return!1}else if(e<n.left)return!1;return!0}E[1]=E.ColumnHead,E[0]=E.GroupPanelHead;const q=".dxbs-table td.table-active",D=".dropdown-item.active",A=".dropdown-item.focused",U="--selection-bg",W="--component-width",P="--scroll-left",_=new ResizeObserver((e=>{for(let t=0;t<e.length;t++){const o=e[t],n=o.target;n.style.setProperty(W,o.contentRect.width+"px"),n._dxOnWindowResize&&n._dxOnWindowResize()}}));class k{constructor(e,t,o){this._itemHeight=e,this._scrollTop=t,this._scrollHeight=o}get itemHeight(){return this._itemHeight}get scrollTop(){return this._scrollTop}get scrollHeight(){return this._scrollHeight}isEqual(e){return this.itemHeight===e.itemHeight&&this.scrollTop===e.scrollTop&&this.scrollHeight===e.scrollHeight}toJSON(){return{ItemHeight:this.itemHeight,ScrollTop:this.scrollTop,ScrollHeight:this.scrollHeight}}}function O(e){if(!c(e))return;let t=function(e){let t=e.querySelector("*[id$='_LB']");!t&&e.parentNode&&(t=e.parentNode.querySelector("*[id$='_LB']"));t||(t=e);if(t){let e=t.querySelector(D);if(e||(e=t.querySelector(q)),e)return e.parentNode}return null}(e);if(t||(t=function(e){let t=null;a(e,(o=>{t=e.querySelector("*"+o+" > *[id$='_LB']")})),t||a(e.parentNode,(o=>{e.parentNode&&(t=e.parentNode.querySelector("*"+o+" > *[id$='_LB']"))}));const o=t?t.querySelector(q):null;return o?o.parentNode:null}(e)),t){const o=e;let n=t.offsetTop;if(t.offsetParent&&"TABLE"===t.offsetParent.tagName){const e=t.offsetParent.previousElementSibling;e&&(n+=e.clientHeight)}const l=o.scrollTop+o.clientHeight<n+t.offsetHeight;o.scrollTop>n&&(o.scrollTop=n),l&&(o.scrollTop=n-(o.clientHeight-t.offsetHeight))}}function z(e){if(!c(e))return;const t=function(e){let t=e.querySelector("*[id$='_LB']");!t&&e.parentNode&&(t=e.parentNode.querySelector("*[id$='_LB']"));t||(t=e);if(t){const e=t.querySelector(A);if(e)return"TR"===e.tagName?e:e.parentNode}return null}(e);if(t){let o=e.querySelector(".dxgvCSD");o||(o=e);const n=o.scrollTop+o.clientHeight<t.offsetTop+t.offsetHeight;o.scrollTop>t.offsetTop&&(o.scrollTop=t.offsetTop),n&&(o.scrollTop=t.offsetTop-(o.clientHeight-t.offsetHeight))}}function G(e){void 0===e.dataset.virtualScrollLock&&(e.dataset.virtualScrollLock="0")}function j(e){G(e);const t=Number(e.dataset.virtualScrollLock)+1;e.dataset.virtualScrollLock=t.toString()}function X(e){G(e);const t=Number(e.dataset.virtualScrollLock)-1;e.dataset.virtualScrollLock=t.toString()}function F(e,t,o,n,l,r,i){o.isHorizontalScrolling&&$(e,n,l),o.isVirtualScrolling&&function(e,t,o,n,l){if(r=o,G(r),Number(r.dataset.virtualScrollLock)>0)return;var r;!function(e,t,o,n,l){let r=!0;o.dataset.prevScrollTop?r=o.dataset.prevScrollTop!==o.scrollTop.toString():o.dataset.prevScrollTop=o.scrollTop.toString();Y(o),r&&(o.dataset.OnScrollTimerId=setTimeout((function(){(function(e,t,o,n){const l=J(t),r=ee(t),i=r.scrollTop,s=r.scrollBottom,c=n.clientHeight>0&&s>o.offsetHeight+l.offsetHeight;return o.clientHeight>0&&i<o.offsetHeight||c?function(e,t,o){const n=e;return n.dxScrollStateCache&&n.dxScrollStateCache.isEqual(o)?Promise.resolve():(n.dxScrollStateCache=o,t.DxRestoreScrollTop=t.scrollTop,j(t),e.invokeMethodAsync("OnGridVirtualScroll",o.itemHeight,o.scrollTop,o.scrollHeight).then((o=>{!function(e,t){de(e.mainElement,e,t)}(o,e),X(t)})).catch(te))}(e,t,r.requestScrollState):Promise.resolve()})(e,o,n,l).then((()=>{delete o.dataset.prevScrollTop})),t.needInternalSettings&&ne(t)}),200).toString(),function(e){const t=0===e.scrollTop,o=e.scrollHeight-e.scrollTop===e.clientHeight,n="dx-scrolling";e.classList.remove(n),t||o||e.classList.add(n)}(o))}(e,t,o,n,l)}(t,o,n,r,i)}function $(e,t,n){if(t.scrollLeft===n.scrollLeft)return;const l=o.EvtUtils.getEventSource(e);if(l===t){const e=t.scrollLeft;n.scrollLeft=e,t.style.setProperty(P,e+"px")}else l===n&&setTimeout((()=>t.scrollLeft=n.scrollLeft),0)}function Y(e){e.dataset.OnScrollTimerId&&(clearTimeout(Number(e.dataset.OnScrollTimerId)),delete e.dataset.OnScrollTimerId)}function J(e){const t=e.querySelector("table.dxbs-table"),o=e.classList.contains("dxbs-listbox")?e.querySelector("ul"):null;return t||o}function K(e,t,o,n,l){const r=J(t),i=function(e,t){const o=Z(e);return{itemHeight:o,spacerTop:t.virtualScrollingOptions.itemsAbove*o,spacerBelow:t.virtualScrollingOptions.itemsBelow*o}}(r,l);o.style.height=i.spacerTop+"px",n.style.height=i.spacerBelow+"px",function(e,t,o){e.scrollTop<o.clientHeight&&(e.scrollTop=o.clientHeight+1);e.scrollTop+e.clientHeight>o.clientHeight+t.offsetHeight&&(e.scrollTop=o.clientHeight+t.offsetHeight-e.clientHeight-1)}(t,r,o)}function Q(e){return Array.from(function(e){switch(e.tagName){case"TABLE":return e.querySelectorAll(":scope > tbody > tr");case"UL":return e.querySelectorAll(":scope > li");default:throw new Error("Unexpected data container element")}}(e),(e=>e.offsetHeight))}function Z(e){const t=Q(e),o={};for(let e=0;e<t.length;e++){const n=t[e];o[n]=o[n]?o[n]+1:1}let n=0,l=0;for(const e in o)o[e]>l&&(l=o[e],n=Number(e));return n}function ee(e){const t=300,o=J(e);let n=e.scrollTop-t;n<0&&(n=0);const l=e.scrollTop+e.clientHeight+t;let r=n-t;r<0&&(r=0);const i=l-r+t,s=Z(o);return{scrollTop:n,scrollBottom:l,requestScrollState:new k(s,r,i)}}function te(e){e&&e.mainElement&&ue(e.mainElement)}function oe(e){return()=>{if(!e)return;const t=e.parentStyleSheet;if(!t)return;const o=Array.prototype.indexOf.call(t.cssRules,e);o>-1&&t.deleteRule(o)}}function ne(e){const t=e.elementsStorage,o=d(e.mainElement).parentElement;if(!o)return null;const n=d(t.scrollElement),l=d(t.scrollHeaderElement),r=[],i=window.getComputedStyle(o);if(!i)return null;if(!n.style.maxHeight)if(e.isDropDown)n.style.maxHeight=ie(i,l)+"px";else{const e=g(o,(e=>{n.style.maxHeight=e.height-l.offsetHeight+"px"})),t=g(l,(e=>{n.style.maxHeight=o.clientHeight-(e.height+2*m(l))+"px"}));r.push((()=>{u(e),u(t)})),n.style.maxHeight=o.clientHeight-l.offsetHeight+"px"}if(e.isDropDown&&2!==e.dropDownWidthMode){const t=re(n,l,o,e,i),s=n.querySelector("table");if(!s)return null;x(s,(()=>{t&&t()}));const c=g(s,(t=>{v(s);const r=re(n,l,o,e,i);x(s,(function(){r&&r()}))}));r.push((function(){u(c),v(s)}))}return r.length>0?()=>{r.forEach((e=>e()))}:null}function le(e,o,n){const l="dxbs-vertical-scrollbar-visible";o&&!n?t.DomUtils.addClassName(e,l):t.DomUtils.removeClassName(e,l)}function re(e,t,o,n,l){function r(e,t){const o=e.querySelector(t);return o?o.children:null}let i=null;const s=e.querySelector("table"),c=t.querySelector("table");if(!s||!c)return null;const a=r(s,"tbody>tr"),u=a&&1===a.length&&s.querySelector("tr.dxbs-empty-data-row"),f=r(c,"colgroup"),p=r(s,"colgroup");o.dataset.calculated&&function(e,t){if(!e||!t)return;for(let o=0;o<e.length;o++){const n=e[o];n.dataset.autoWidth&&ce(n,t.item(o))}}(f,p);let g=0;if(u){c.style.width="auto",c.style.tableLayout="auto";const e=window.getComputedStyle(c).width;c.removeAttribute("style"),g=parseFloat(e)}else{const e=r(c,"thead>tr");if(!(f&&e&&a&&p))return null;s.style.width=c.style.width="auto",s.style.tableLayout=c.style.tableLayout="auto";const t=[];for(let o=0;o<f.length;o++){const l=f[o];if(l.style.width)if(-1!==l.style.width.indexOf("%"))t.push(o);else{const e=S(),t=d(n.mainElement).getAttribute("data-dxdg-id");let r=null;e&&(e.insertRule("[data-dxdg-id='"+t+"'] table tr>td:nth-child("+(o+1)+"), [data-dxdg-id='"+t+"'] table tr>th:nth-child("+(o+1)+") { max-width:"+l.style.width+"; }",e.cssRules.length),r=e.cssRules[e.cssRules.length-1],g+=parseFloat(l.style.width)),i=oe(r)}else l.dataset.autoWidth="true",g+=se(l,p[o],e[o],a[o])}if(t.length>0)for(let o=0;o<t.length;o++)g+=se(f[o],p[o],e[o],a[o]);s.removeAttribute("style"),c.removeAttribute("style")}if(0===n.dropDownWidthMode||1===n.dropDownWidthMode){const r=n.editor;if(!r)return null;const i=parseInt(l.borderRightWidth,10)+parseInt(l.borderLeftWidth,10),s=g+(e.querySelector("table").offsetHeight>ie(l,t)?h():0)+i;0===n.dropDownWidthMode&&r.offsetWidth>s?(!function(e,t,o){if(!e||!t)return;const n=Array.from(e),l=n.filter((e=>e.dataset.autoWidth));if(l.length>0){const e=Math.floor(o/l.length);for(let o=0;o<l.length-1;o++){const r=l[o],i=t[n.indexOf(r)],s=Number.parseInt(r.style.width);i.style.width=r.style.width=s+e+"px"}const r=l[l.length-1];ce(r,t[n.indexOf(r)])}}(f,p,r.offsetWidth-s),o.style.width=r.offsetWidth-2+"px"):o.style.width=s+"px"}return o.dataset.calculated="true",i}function ie(e,t){const o=parseInt(e.borderTopWidth,10)+parseInt(e.borderBottomWidth,10);return parseInt(e.maxHeight,10)-o-t.offsetHeight}function se(e,t,o,n){const l=Math.ceil(o.getBoundingClientRect().width),r=Math.ceil(n.getBoundingClientRect().width),i=l>r?l:r;return t.style.width=e.style.width=i+"px",i}function ce(e,t){e.style.width="",t&&(t.style.width="")}k.Auto=new k(0,0,0);const ae=document.createElement("TD");function de(e,l,i){return s=e,c=e=>{const s=i,c=l.isMultipleSelectionEnabled,a=l.scrollToSelectedItemRequested,g=l.elementsStorage;v(e);let m=null,S=null,H=null,E=null,L=null;l.needInternalSettings&&(L=ne(l));const N=d(g.scrollElement),I=d(g.scrollHeaderElement);if(N){const o=d(g.virtualScrollSpacerTopElement),n=d(g.virtualScrollSpacerBottomElement);if(j(N),(l.isVirtualScrolling||l.isVerticalScrolling)&&(t.DomUtils.addClassName(e,"dxbs-has-vertical-scrollbar"),le(e,N.scrollHeight>N.clientHeight,l.isHorizontalScrolling),l.isFirstScrollableRender&&l.isAutoVerticalScrollBarMode&&(e.disposeVerticalScrollBarSubscriber=function(e,t,o){const n=y(t,(t=>{le(e,t,o)}));return()=>{u(n)}}(e,N,l.isHorizontalScrolling)),l.isFirstScrollableRender&&(e.disposeVerticalScrollBarWidthSubscriber=function(){const e=b((()=>w()));return()=>{u(e)}}())),l.isVirtualScrolling&&(K(0,N,o,n,l),a?O(N):N.DxRestoreScrollTop&&(N.scrollTop=N.DxRestoreScrollTop,delete N.DxRestoreScrollTop)),function(e){const t=e.querySelectorAll(".btn.btn-toggle");if(0!==t.length)for(let o=0;o<t.length;o++){const n=t[o],l=n.offsetWidth+p(n.parentNode);if(l>0){requestAnimationFrame((()=>{e.style.setProperty("--button-w",l+"px")}));break}}}(e),m=e=>F(e,s,l,N,I,o,n),N.addEventListener("scroll",m),I&&I.addEventListener("scroll",m),X(N),function(e){return!e.needInternalSettings&&(e.isHorizontalScrolling||e.isVerticalScrolling&&e.columnResizeMode!==T.Component)}(l)){const t=d(g.rootElement);S=()=>function(e,t,o,n){let l="",r="";t&&(j(t),l=t.style.overflowX,t.style.overflowX="hidden",t.style.width="0"),o&&(r=o.style.overflowX,o.style.overflowX="hidden",o.style.width="0");const i=e.clientWidth;if(t&&(i&&(t.style.width=i+"px"),t.style.overflowX=l),o){const e=function(e,t){return(t.isVerticalScrolling||t.isVirtualScrolling)&&(e.clientHeight<e.scrollHeight||"scroll"===e.style.overflowY)}(t,n)?h():0;i&&(o.style.width=i-e+"px"),o.style.overflowX=r}t&&X(t)}(t,N,I,l),S(),window.addEventListener("resize",S),e._dxOnWindowResize=S}}function B(e){M(e)}function M(e){if(!c||!e.shiftKey||!o.EvtUtils.isLeftButtonPressed(e))return;const t=o.EvtUtils.getEventSource(e);r(t,"dxbs-data-row")&&f()}e.addEventListener("mousedown",B);let V=null,C=null;const q=l.columnResizeMode!==T.Disabled;if(l.isColumnDragEnabled){const t=e.dataset.dxdgId;t&&(V=e.querySelector("[data-dxdg-drag-head-row='"+t+"']"),V&&(H=e=>R(e,t,q,s),V.addEventListener(n.TouchUtils.touchMouseDownEventName,H)),C=e.querySelector("[data-dxdg-gp='"+t+"']"),C&&(E=e=>R(e,t,q,s),C.addEventListener(n.TouchUtils.touchMouseDownEventName,E)))}if(x(e,(function(){L&&L(),m&&(N.removeEventListener("scroll",F),I&&I.removeEventListener("scroll",F)),S&&window.removeEventListener("resize",S),N&&Y(N),H&&V&&V.removeEventListener(n.TouchUtils.touchMouseDownEventName,H),E&&C&&C.removeEventListener(n.TouchUtils.touchMouseDownEventName,E),e.removeEventListener("mousedown",B)})),l.isFirstScrollableRender&&l.isVirtualScrolling&&l.virtualScrollingOptions.itemsBelow>0){const e=ee(N);return JSON.stringify(e.requestScrollState)}return JSON.stringify(k.Auto)},new Promise(((e,o)=>{const n=d(s);if(!n)return o("failed");n._dxResizeAttached||(n._dxResizeAttached=!0,_.observe(n)),cancelAnimationFrame(n._dxNextRafId||-1);const l=n.clientWidth+"px",r=t.DomUtils.getCurrentStyle(ae).backgroundColor;try{const t=c(n);n._dxNextRafId=requestAnimationFrame((()=>{n.style.setProperty(U,r),n.style.setProperty(W,l),e(t)}))}catch(e){o(`DxDataGrid.Init error: ${e}`)}}));var s,c}function ue(e){return(e=d(e))&&function(e){_.unobserve(e),v(e),function(e){e.disposeVerticalScrollBarSubscriber&&(e.disposeVerticalScrollBarSubscriber(),delete e.disposeVerticalScrollBarSubscriber)}(e),function(e){e.disposeVerticalScrollBarWidthSubscriber&&(e.disposeVerticalScrollBarWidthSubscriber(),delete e.disposeVerticalScrollBarWidthSubscriber)}(e)}(e),Promise.resolve("ok")}ae.style.cssText="display: none; position: fixed; top: -1000px; left: -1000px;",ae.className="table-active",document.body.appendChild(ae);const fe={init:de,dispose:ue};export{fe as default,ue as dispose,ee as getParametersForVirtualScrollingRequest,de as init,z as scrollToFocusedItem,O as scrollToSelectedItem};