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

    if (!canvas) {
        console.error("Error: Elemento canvas no proporcionado");
        return;
    }

    const ctx = canvas.getContext('2d');
    if (!ctx) {
        console.error("Error: No se pudo obtener contexto 2D");
        return;
    }

    if (!calle) {
        console.error("Error: Datos de calle no proporcionados");
        return;
    }

    console.log("Dibujando calle:", calle);

    let congestion = (calle.ConteoActualVehiculos / 20) * 100;
    ctx.beginPath();

    if (calle.Informacion1 == "Nodo 5" || calle.Informacion2 == "Nodo 5") {
        ctx.strokeStyle = '#444'; // gris oscuro
        ctx.lineWidth = 30; // grosor de la carretera
        ctx.lineCap = 'square';

        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        console.log("Semáforo:", calle.Informacion1, "→", calle.Informacion2, "| Color:", calle.ColorSemaforo);

        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = calle.ColorSemaforo || '#2ecc71'; // usa color enviado desde Blazor
        ctx.lineWidth = 24;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        console.log("Semáforo:", calle.Informacion1, "→", calle.Informacion2, "| Color:", calle.ColorSemaforo);

        ctx.stroke();


    }else {
        ctx.strokeStyle = '#444'; // gris oscuro
        ctx.lineWidth = 25; // grosor de la carretera
        ctx.lineCap = 'square';

        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        console.log("Semáforo:", calle.Informacion1, "→", calle.Informacion2, "| Color:", calle.ColorSemaforo);

        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = calle.ColorSemaforo || '#2ecc71'; // usa color enviado desde Blazor
        ctx.lineWidth = 24;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        console.log("Semáforo:", calle.Informacion1, "→", calle.Informacion2, "| Color:", calle.ColorSemaforo);

        ctx.stroke();

    }

    // 3. Dibujar la línea discontinua blanca encima
    ctx.beginPath();
    ctx.setLineDash([20, 20]); // patrón discontínuo
    ctx.strokeStyle = 'white';
    ctx.lineWidth = 3;
    ctx.moveTo(calle.DesdeX, calle.DesdeY);
    ctx.lineTo(calle.HastaX, calle.HastaY);
    ctx.stroke();
    ctx.setLineDash([]); 
    //  dibujarFlecha(ctx, calle.DesdeX, calle.DesdeY, calle.HastaX, calle.HastaY);

}

function dibujarFlecha(ctx, fromX, fromY, toX, toY) {
    const headLength = 15; // Longitud de la punta de la flecha
    const angle = Math.atan2(toY - fromY, toX - fromX);

    ctx.fillStyle = ctx.strokeStyle; // Mismo color que la línea

    // Dibujar punta de flecha
    ctx.beginPath();
    ctx.moveTo(toX, toY);
    ctx.lineTo(
        toX - headLength * Math.cos(angle - Math.PI / 6),
        toY - headLength * Math.sin(angle - Math.PI / 6)
    );
    ctx.lineTo(
        toX - headLength * Math.cos(angle + Math.PI / 6),
        toY - headLength * Math.sin(angle + Math.PI / 6)
    );
    ctx.closePath();
    ctx.fill();
}

export function dibujarNodoIndividual(canvas, nodoRecibido) {
    let nodo = JSON.parse(nodoRecibido);
    nodo = nodo[0];
    console.log("Intentando dibujar nodo:", nodo);
    console.log(nodo.PosX + " y " + nodo.PosY)
    const ctx = canvas.getContext('2d');
    if (!ctx) return;


    ctx.fillStyle = '#3498db';

    ctx.beginPath();
    ctx.arc(nodo.PosX, nodo.PosY, 20, 0, Math.PI * 2);
    ctx.fill();

    // Texto del nodo
    ctx.fillStyle = 'white';
    ctx.font = 'bold 22px Arial';
    ctx.textAlign = 'center';
    ctx.fillText(nodo.Informacion, nodo.PosX, nodo.PosY + 7);

    console.log("Nodo dibujado:", nodo.Informacion);

    // Mostrar conteo de vehiculos esperando
    if (nodo.ConteoActualVehiculos > 0) {
        ctx.fillStyle = 'black';
        ctx.font = 'bold 14px Arial';
        ctx.textAlign = 'center';
        ctx.fillText(`🚗 ${nodo.ConteoActualVehiculos}`, nodo.PosX, nodo.PosY + 30);
    }
}

export function dibujarRutaDeNodos(stringDeNodos, canvasRuta) {
    let ctxRuta = canvasRuta.getContext('2d');
    if (!ctxRuta) return;

    stringDeNodos = stringDeNodos.trim().replace(/;$/, '');
    let arregloDeNodos = stringDeNodos.split(";");

    let espacioX = 70; // espacio horizontal
    let x = 50;         // posición inicial x
    let y = 50;         // posición fija y
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

    
