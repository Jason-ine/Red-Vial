export function limpiarCanvas(canvas) {
    if (!canvas) {
        console.error("Elemento canvas no existe");
        return;
    }

    const ctx = canvas.getContext('2d');
    if (!ctx) {
        console.error("No se pudo obtener contexto 2D");
        return;
    }

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    console.log("Canvas limpiado");
}

export function dibujarCalleIndividual(canvas, calleRecibida) {
    let calle = JSON.parse(calleRecibida);
    calle = calle[0];

    if (!canvas || !calle) return;

    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    let congestion = (calle.conteoActualVehiculos / 20) * 100;

    ctx.beginPath();

    if (calle.informacion1 === "5" || calle.informacion2 === "5") {
        ctx.strokeStyle = '#444';
        ctx.lineWidth = 30;
        ctx.lineCap = 'square';

        ctx.moveTo(calle.desdeX, calle.desdeY);
        ctx.lineTo(calle.hastaX, calle.hastaY);
        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = calle.colorSemaforo || '#2ecc71';
        ctx.lineWidth = 24;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.desdeX, calle.desdeY);
        ctx.lineTo(calle.hastaX, calle.hastaY);
        ctx.stroke();
    } else {
        ctx.strokeStyle = '#444';
        ctx.lineWidth = 25;
        ctx.lineCap = 'square';

        ctx.moveTo(calle.desdeX, calle.desdeY);
        ctx.lineTo(calle.hastaX, calle.hastaY);
        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = calle.colorSemaforo || '#2ecc71';
        ctx.lineWidth = 24;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.desdeX, calle.desdeY);
        ctx.lineTo(calle.hastaX, calle.hastaY);
        ctx.stroke();
    }

    // Línea discontinua blanca
    ctx.beginPath();
    ctx.setLineDash([20, 20]);
    ctx.strokeStyle = 'white';
    ctx.lineWidth = 3;
    ctx.moveTo(calle.desdeX, calle.desdeY);
    ctx.lineTo(calle.hastaX, calle.hastaY);
    ctx.stroke();
    ctx.setLineDash([]);
}

export function dibujarNodoIndividual(canvas, nodoRecibido) {
    let nodo = JSON.parse(nodoRecibido);
    nodo = nodo[0];

    if (!canvas) {
        return;
    }

    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    // Nodo base (círculo azul)
    ctx.fillStyle = '#3498db';
    ctx.beginPath();
    ctx.arc(nodo.posX, nodo.posY, 20, 0, Math.PI * 2);
    ctx.fill();
    // Número del nodo
    ctx.fillStyle = 'white';
    ctx.font = 'bold 22px Arial';
    ctx.textAlign = 'center';
    ctx.fillText(nodo.informacion, nodo.posX, nodo.posY + 7);

    // Contador de vehículos esperando (arriba del nodo)
    if (nodo.conteoActualVehiculos && nodo.conteoActualVehiculos > 0) {
        // Fondo blanco circular
        ctx.beginPath();
        ctx.arc(nodo.posX, nodo.posY - 30, 14, 0, Math.PI * 2);
        ctx.fillStyle = 'white';
        ctx.fill();

        // Texto del contador
        ctx.fillStyle = 'black';
        ctx.font = 'bold 14px Arial';
        ctx.textAlign = 'center';
        ctx.fillText(`🚗 ${nodo.conteoActualVehiculos}`, nodo.posX, nodo.posY - 26);
    }
}
export function dibujarRutaDeNodos(stringDeNodos, canvasRuta) {
    if (!canvasRuta) {
        console.error("Elemento canvas no existe");
        return;
    }
    let ctxRuta = canvasRuta.getContext('2d');
    if (!ctxRuta) return;

    stringDeNodos = stringDeNodos.trim().replace(/;$/, '');
    let arregloDeNodos = stringDeNodos.split(";");

    let espacioX = 70; 
    let x = 50;         
    let y = 50;        
    let radio = 20;

    for (let i = 0; i < arregloDeNodos.length; i++) {
        // Dibuja el nodo (círculo)
        ctxRuta.fillStyle = '#3498db';
        ctxRuta.beginPath();
        ctxRuta.arc(x, y, radio, 0, Math.PI * 2);
        ctxRuta.fill();

        // Dibuja el texto del nodo
        ctxRuta.fillStyle = 'white';
        ctxRuta.font = 'bold 12px Arial';
        ctxRuta.textAlign = 'center';
        ctxRuta.textBaseline = 'middle';
        ctxRuta.fillText(arregloDeNodos[i], x, y);

        // Si no es el último nodo, dibuja una flecha hacia el siguiente
        if (i < arregloDeNodos.length - 1) {
            let inicioX = x + radio;
            let finX = x + espacioX - radio;
            let puntaX = finX;
            let puntaY = y;

            // Línea
            ctxRuta.strokeStyle = '#000';
            ctxRuta.lineWidth = 2;
            ctxRuta.beginPath();
            ctxRuta.moveTo(inicioX, y);
            ctxRuta.lineTo(finX, y);
            ctxRuta.stroke();

            // Cabeza de flecha
            let tamFlecha = 6;
            ctxRuta.beginPath();
            ctxRuta.moveTo(puntaX, puntaY);
            ctxRuta.lineTo(puntaX - tamFlecha, puntaY - tamFlecha);
            ctxRuta.lineTo(puntaX - tamFlecha, puntaY + tamFlecha);
            ctxRuta.closePath();
            ctxRuta.fillStyle = '#000';
            ctxRuta.fill();
        }

        x += espacioX;
    }
}
