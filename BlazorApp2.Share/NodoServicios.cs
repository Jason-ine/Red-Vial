using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Share
{
    public class NodoServicios
    {
        private Nodo NodoInicio;

        public NodoServicios()
        {
            NodoInicio = null;
        }

        public void CrearNodoInicio(Nodo nodo)
        {
            NodoInicio = nodo;
        }

        public Nodo ObtenerNodoInicio()
        {
            return NodoInicio;
        }

        private Boolean NodoVacio(Nodo nodo)
        {
            if (nodo == null)
            {
                Console.WriteLine("Nodo no encontrado.");
            }
            return nodo == null;
        }

        public void AgregarNodo(Nodo nodoReferencia, string direccion, Nodo nuevoNodo)
        {
            if (nodoReferencia == null)
            {
                Console.WriteLine("Error: El nodo de referencia no existe.");
                return;
            }

            switch (direccion.ToLower())
            {
                case "arriba":
                    if (nodoReferencia.ReferenciaArriba == null)
                    {
                        nodoReferencia.ReferenciaArriba = nuevoNodo;
                        Console.WriteLine("Nodo agregado ARRIBA");
                    }
                    else
                    {
                        Console.WriteLine("Ya existe un nodo arriba.");
                    }
                    break;
                case "abajo":
                    if (nodoReferencia.ReferenciaAbajo == null)
                    {
                        nodoReferencia.ReferenciaAbajo = nuevoNodo;
                        Console.WriteLine("Nodo agregado ABAJO");
                    } else
                    {
                        Console.WriteLine("Ya existe un nodo abajo.");
                    }
                    break;
                case "izquierda":
                    if (nodoReferencia.ReferenciaIzquierda == null)
                    {
                        nodoReferencia.ReferenciaIzquierda = nuevoNodo;
                        Console.WriteLine("Nodo agregado IZQUIERDA");
                    }
                    else
                    {
                        Console.WriteLine("Ya existe un nodo izquierda.");
                    }
                    break;
                case "derecha":
                    if (nodoReferencia.ReferenciaDerecha == null)
                    {
                        nodoReferencia.ReferenciaDerecha = nuevoNodo;
                        Console.WriteLine("Nodo agregado DERECHA");
                    }
                    else
                    {
                        Console.WriteLine("Ya existe un nodo derecha.");
                    }
                    break;
                default:
                    Console.WriteLine("Dirección inválida. Indicar: arriba, abajo, izquierda o derecha.");
                    break;

            }
        }

        public void ModificarInformacion(Nodo nodo, object nuevaInformacion)
        {
            if (NodoVacio(nodo)) return;
            
            nodo.Informacion = nuevaInformacion;
            Console.WriteLine("Información actualizada");
        }
        
        public void ActivarSemaforo(Nodo nodo)
        {
            if (NodoVacio(nodo)) return;

            nodo.TieneSemaforo = true;
            Console.WriteLine("Semaforo activado.");
        }

        public void DesactivarSemaforo(Nodo nodo)
        {
            if (NodoVacio(nodo)) return;

            nodo.TieneSemaforo = false;
            Console.WriteLine("Semaforo desactivado.");
        }

        public void ActualizarConteoVehiculos(Nodo nodo, int nuevoConteoActual, int nuevoConteoHistorico)
        {
            if (NodoVacio(nodo)) return;

            nodo.ConteoActualVehiculos = nuevoConteoActual;
            nodo.ConteoHistoricoVehiculos = nuevoConteoHistorico;
            Console.WriteLine("Conteo de vehiculos actualizado");
        }

    }
}
