function dibujarFlecha(ctx, fromX, fromY, toX, toY, isDoubleWay) {
    const headLength = 10;
    const arrowColor = 'white';
    ctx.fillStyle = arrowColor;
    ctx.strokeStyle = arrowColor;

    const angle = Math.atan2(toY - fromY, toX - fromX);

    if (isDoubleWay) {
        const midX = (fromX + toX) / 2;
        const midY = (fromY + toY) / 2;

        ctx.beginPath();
        ctx.moveTo(midX, midY);
        ctx.lineTo(
            midX + headLength * Math.cos(angle - Math.PI / 6),
            midY + headLength * Math.sin(angle - Math.PI / 6)
        );
        ctx.lineTo(
            midX + headLength * Math.cos(angle + Math.PI / 6),
            midY + headLength * Math.sin(angle + Math.PI / 6)
        );
        ctx.closePath();
        ctx.fill();

        ctx.beginPath();
        ctx.moveTo(midX, midY);
        const oppositeAngle = angle + Math.PI; // Calcula el ángulo opuesto
        ctx.lineTo(
            (midX)  + headLength * Math.cos(oppositeAngle - Math.PI / 6),
            midY + headLength * Math.sin(oppositeAngle - Math.PI / 6)
        );
        ctx.lineTo(
            midX + headLength * Math.cos(oppositeAngle + Math.PI / 6),
            midY + headLength * Math.sin(oppositeAngle + Math.PI / 6)
        );
        ctx.closePath();
        ctx.fill();
    } else {
        // Dibuja una flecha al final para las calles de una vía
        const midX = (fromX + toX) / 2;
        const midY = (fromY + toY) / 2;
        ctx.beginPath();
        ctx.moveTo(midX, midY);
        ctx.lineTo(
            midX - headLength * Math.cos(angle - Math.PI / 6),
            midY - headLength * Math.sin(angle - Math.PI / 6)
        );
        ctx.lineTo(
            midX - headLength * Math.cos(angle + Math.PI / 6),
            midY - headLength * Math.sin(angle + Math.PI / 6)
        );
        ctx.closePath();
        ctx.fill();
    }
}

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

export function dibujarNodoIndividual(canvas, nodoRecibido) {
    let nodo = JSON.parse(nodoRecibido);
    nodo = nodo[0];
    console.log("Intentando dibujar nodo:", nodo);
    console.log(nodo.PosX + " y " + nodo.PosY)

    const ctx = canvas.getContext('2d');
    if (!ctx) return;


    ctx.fillStyle = '#3498db';

    ctx.beginPath();
    ctx.arc(nodo.PosX - 30, nodo.PosY - 35, 20, 0, Math.PI * 2);
    ctx.fill();

    ctx.fillStyle = "black";
    ctx.font = "10px Arial";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(nodo.Informacion, nodo.PosX - 30, nodo.PosY - 35)
    // Dibujar semáforo si existe
    if (nodo.TieneSemaforo) {
        ctx.fillStyle = '#f1c40f';
        ctx.fillRect(nodo.PosX - 8, nodo.PosY - 8, 16, 16);
    }

    // Texto del nodo
    ctx.fillStyle = 'white';
    ctx.font = 'bold 12px Arial';
    ctx.textAlign = 'center';
    ctx.fillText(nodo.Informacion, nodo.PosX, nodo.PosY + 5);

    console.log("Nodo dibujado:", nodo.Informacion);
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

    const esDobleVia = (calle.Informacion1 == "Nodo 5" || calle.Informacion2 == "Nodo 5");

    if (esDobleVia) {
        ctx.strokeStyle = '#444';
        ctx.lineWidth = 30;
        ctx.lineCap = 'square';

        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = congestion > 70 ? '#e74c3c' :
            congestion > 40 ? '#f39c12' :
                '#2ecc71';
        ctx.lineWidth = 29;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        ctx.stroke();

        // Dibuja flechas para doble vía en el centro
        dibujarFlecha(ctx, calle.DesdeX, calle.DesdeY, calle.HastaX, calle.HastaY, true);

    } else {
        ctx.strokeStyle = '#444';
        ctx.lineWidth = 20;
        ctx.lineCap = 'square';

        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        ctx.stroke();

        ctx.beginPath();
        ctx.strokeStyle = congestion > 70 ? '#e74c3c' :
            congestion > 40 ? '#f39c12' :
                '#2ecc71';
        ctx.lineWidth = 19;
        ctx.lineCap = 'square';
        ctx.moveTo(calle.DesdeX, calle.DesdeY);
        ctx.lineTo(calle.HastaX, calle.HastaY);
        ctx.stroke();

        // Dibuja flechas para una vía al final
        dibujarFlecha(ctx, calle.DesdeX, calle.DesdeY, calle.HastaX, calle.HastaY, false);
    }

    // Línea discontinua
    ctx.beginPath();
    ctx.setLineDash([10, 10]);
    ctx.strokeStyle = 'white';
    ctx.lineWidth = 3;
    ctx.moveTo(calle.DesdeX, calle.DesdeY);
    ctx.lineTo(calle.HastaX, calle.HastaY);
    ctx.stroke();
    ctx.setLineDash([]);


}
