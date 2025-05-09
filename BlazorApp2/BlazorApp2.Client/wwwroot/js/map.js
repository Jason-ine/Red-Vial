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

    // Dibujar semáforo si existe
    //if (nodo.TieneSemaforo) {
     //   ctx.fillStyle = '#f1c40f';
       // ctx.fillRect(nodo.PosX - 8, nodo.PosY - 8, 16, 16);
    //}

    // Texto del nodo
    ctx.fillStyle = 'white';
    ctx.font = 'bold 22px Arial';
    ctx.textAlign = 'center';
    ctx.fillText(nodo.Informacion, nodo.PosX, nodo.PosY + 7);



    console.log("Nodo dibujado:", nodo.Informacion);
}