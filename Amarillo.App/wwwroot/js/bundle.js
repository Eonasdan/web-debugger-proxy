var app=function(){"use strict";function t(){}function e(t){return t()}function n(){return Object.create(null)}function o(t){t.forEach(e)}function r(t){return"function"==typeof t}function s(t,e){return t!=t?e==e:t!==e||t&&"object"==typeof t||"function"==typeof t}function c(t,e){t.appendChild(e)}function i(t,e,n){t.insertBefore(e,n||null)}function l(t){t.parentNode.removeChild(t)}function a(t){return document.createElement(t)}function u(t){return document.createTextNode(t)}function d(){return u(" ")}function f(t,e,n){null==n?t.removeAttribute(e):t.getAttribute(e)!==n&&t.setAttribute(e,n)}function h(t,e){e=""+e,t.wholeText!==e&&(t.data=e)}let g;function p(t){g=t}const m=[],b=[],$=[],y=[],x=Promise.resolve();let w=!1;function v(t){$.push(t)}const _=new Set;let M=0;function A(){const t=g;do{for(;M<m.length;){const t=m[M];M++,p(t),E(t.$$)}for(p(null),m.length=0,M=0;b.length;)b.pop()();for(let t=0;t<$.length;t+=1){const e=$[t];_.has(e)||(_.add(e),e())}$.length=0}while(m.length);for(;y.length;)y.pop()();w=!1,_.clear(),p(t)}function E(t){if(null!==t.fragment){t.update(),o(t.before_update);const e=t.dirty;t.dirty=[-1],t.fragment&&t.fragment.p(t.ctx,e),t.after_update.forEach(v)}}const k=new Set;function C(t,e){-1===t.$$.dirty[0]&&(m.push(t),w||(w=!0,x.then(A)),t.$$.dirty.fill(0)),t.$$.dirty[e/31|0]|=1<<e%31}function R(s,c,i,a,u,d,f,h=[-1]){const m=g;p(s);const b=s.$$={fragment:null,ctx:null,props:d,update:t,not_equal:u,bound:n(),on_mount:[],on_destroy:[],on_disconnect:[],before_update:[],after_update:[],context:new Map(c.context||(m?m.$$.context:[])),callbacks:n(),dirty:h,skip_bound:!1,root:c.target||m.$$.root};f&&f(b.root);let $=!1;if(b.ctx=i?i(s,c.props||{},((t,e,...n)=>{const o=n.length?n[0]:e;return b.ctx&&u(b.ctx[t],b.ctx[t]=o)&&(!b.skip_bound&&b.bound[t]&&b.bound[t](o),$&&C(s,t)),e})):[],b.update(),$=!0,o(b.before_update),b.fragment=!!a&&a(b.ctx),c.target){if(c.hydrate){const t=function(t){return Array.from(t.childNodes)}(c.target);b.fragment&&b.fragment.l(t),t.forEach(l)}else b.fragment&&b.fragment.c();c.intro&&((y=s.$$.fragment)&&y.i&&(k.delete(y),y.i(x))),function(t,n,s,c){const{fragment:i,on_mount:l,on_destroy:a,after_update:u}=t.$$;i&&i.m(n,s),c||v((()=>{const n=l.map(e).filter(r);a?a.push(...n):o(n),t.$$.on_mount=[]})),u.forEach(v)}(s,c.target,c.anchor,c.customElement),A()}var y,x;p(m)}"function"!=typeof window.external.sendMessage&&(window.external.sendMessage=t=>console.log("Emulating sendMessage.\nMessage sent: "+t)),"function"!=typeof window.external.receiveMessage&&(window.external.receiveMessage=t=>{t("Simulating message from backend.")},window.external.receiveMessage((t=>console.log("Emulating receiveMessage.\nMessage received: "+t))));class N{constructor(){this.photinoWindow=window.external,this.subscribers={},console.log("created message service"),this.photinoWindow.receiveMessage((t=>{try{const e=JSON.parse(t);if(!Array.isArray(this.subscribers[e.Channel]))return;console.log(this.subscribers[e.Channel]),this.subscribers[e.Channel].forEach((t=>{t(e.Body)}))}catch(e){console.log(t),console.error(e)}}))}sendMessage(t){this.photinoWindow.sendMessage(JSON.stringify(t))}subscribe(t,e){Array.isArray(this.subscribers[t])||(this.subscribers[t]=[]),this.subscribers[t].push(e)}}const O=N.instance=new N;var P;function S(t,e,n){const o=t.slice();return o[3]=e[n],o}function T(t){let e,n,o,r,s,f,g,p,m,b,$,y,x,w=t[3].Url+"",v=t[3].Result+"",_=t[3].Method+"",M=t[3].RemoteAddress+"";return{c(){e=a("tr"),n=a("td"),o=u(w),r=d(),s=a("td"),f=u(v),g=d(),p=a("td"),m=u(_),b=d(),$=a("td"),y=u(M),x=d()},m(t,l){i(t,e,l),c(e,n),c(n,o),c(e,r),c(e,s),c(s,f),c(e,g),c(e,p),c(p,m),c(e,b),c(e,$),c($,y),c(e,x)},p(t,e){1&e&&w!==(w=t[3].Url+"")&&h(o,w),1&e&&v!==(v=t[3].Result+"")&&h(f,v),1&e&&_!==(_=t[3].Method+"")&&h(m,_),1&e&&M!==(M=t[3].RemoteAddress+"")&&h(y,M)},d(t){t&&l(e)}}}function j(e){let n,o,r,s,u,h,g,p,m,b,$=e[0],y=[];for(let t=0;t<$.length;t+=1)y[t]=T(S(e,$,t));return{c(){n=a("button"),n.textContent="Toggle",o=d(),r=a("div"),s=a("div"),u=a("div"),h=a("table"),g=a("thead"),g.innerHTML="<tr><td>Url</td> \n                    <td>Result</td> \n                    <td>Method</td> \n                    <td>Remote Address</td></tr>",p=d();for(let t=0;t<y.length;t+=1)y[t].c();f(n,"class","btn btn-success"),f(h,"class","table table-striped"),f(u,"class","col"),f(s,"class","row"),f(r,"class","container-fluid")},m(t,l){i(t,n,l),i(t,o,l),i(t,r,l),c(r,s),c(s,u),c(u,h),c(h,g),c(h,p);for(let t=0;t<y.length;t+=1)y[t].m(h,null);var a,d,f,$;m||(a=n,d="click",f=e[1],a.addEventListener(d,f,$),b=()=>a.removeEventListener(d,f,$),m=!0)},p(t,[e]){if(1&e){let n;for($=t[0],n=0;n<$.length;n+=1){const o=S(t,$,n);y[n]?y[n].p(o,e):(y[n]=T(o),y[n].c(),y[n].m(h,null))}for(;n<y.length;n+=1)y[n].d(1);y.length=$.length}},i:t,o:t,d(t){t&&l(n),t&&l(o),t&&l(r),function(t,e){for(let n=0;n<t.length;n+=1)t[n]&&t[n].d(e)}(y,t),m=!1,b()}}}function L(t,e,n){let o=!1,r=[];O.subscribe(P.Proxy,(t=>{n(0,r=[...r,t])}));return[r,()=>{o=!o,O.sendMessage({Channel:P.Proxy,Command:o?"start":"stop"})}]}!function(t){t.Proxy="Proxy"}(P||(P={}));return new class extends class{$destroy(){!function(t,e){const n=t.$$;null!==n.fragment&&(o(n.on_destroy),n.fragment&&n.fragment.d(e),n.on_destroy=n.fragment=null,n.ctx=[])}(this,1),this.$destroy=t}$on(t,e){const n=this.$$.callbacks[t]||(this.$$.callbacks[t]=[]);return n.push(e),()=>{const t=n.indexOf(e);-1!==t&&n.splice(t,1)}}$set(t){var e;this.$$set&&(e=t,0!==Object.keys(e).length)&&(this.$$.skip_bound=!0,this.$$set(t),this.$$.skip_bound=!1)}}{constructor(t){super(),R(this,t,L,j,s,{})}}({target:document.body})}();
//# sourceMappingURL=bundle.js.map
