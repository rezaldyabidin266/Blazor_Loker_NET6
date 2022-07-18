function mouse_login(){
    var mouse_login = document.querySelector('.mouse-login');
    gsap.set(mouse_login, { xPercent: -50, yPercent: -50 });

    let xTo = gsap.quickTo(mouse_login, "x", { duration: 0.4, ease: "power3" }),
        yTo = gsap.quickTo(mouse_login, "y", { duration: 0.4, ease: "power3" });
    console.log(mouse_login);
    window.addEventListener("mousemove", e => {
        xTo(e.clientX);
        yTo(e.clientY);
    });
}