# Venturi77CallHijacker
 KoiVM,EazVM,AgileVM Patcher - Seguiré actualizando (Esta es una versión apresurada, agregaré todas las características mañana. ¡Esperen muchas actualizaciones!) Guía detallada mañana.

Team Venturi77 Miembros Actuales:

TobitoFatito - https://github.com/TobitoFatitoNulled/
XSilent007 - https://github.com/XSilent007/

# Como usar:
Ejecute el venturi call secuestrador exe y cree una configuración de depuración.

Se debe guardar una configuración en Config.Json del directorio ejecutado.

Ejecute el secuestrador de llamadas nuevamente e inyecte el dll, la detección automática funciona perfectamente.

Ahora, después de haber inyectado el dll, puede ejecutar el .exe en el que lo inyectó y

debería dejar un debug.txt que contenga la información de las llamadas que se manejaron

Ej: https://i.imgur.com/Ekomjuc.png

Ahora puede crear una nueva configuración y, según el txt de depuración, puede

haz que parche las llamadas.

Ejemplo de método que necesita parchear:

https://i.imgur.com/goFjTTm.png

Ejemplo de configuración hecha para el método:

https://i.imgur.com/1Qd1nHH.png

Cuidado: debe tener config.json en el mismo directorio que el .exe.
# Bugs:

Ágil, la inyección Eaz tiene errores, algunos archivos no se inyectan, se solucionarán muy pronto.

Si tiene un programa para inyectar que es eaz / agile y no funciona, puede inyectar

usted mismo con dnlib. ¿Cómo?

En eaz puedes seguir las llamadas con parámetros como este

https://i.imgur.com/YLeVeTm.png

hasta que encuentres esto

https://i.imgur.com/TWq3R3V.png

Luego simplemente Control + F y busca ".Invoke" hasta que encuentre este método:

https://i.imgur.com/2kzcHMj.png

Ahora solo edite las instrucciones, asegúrese de que el dll venturi esté en el mismo directorio

y que lo haya agregado en dnlib y simplemente edite la llamada de esta manera https://i.imgur.com/0Ur15Bz.png

Haz clic en Aceptar https://i.imgur.com/qDfliTe.png y luego asegurate de hacer clic en ".HandleInvoke" para que pueda ver el archivo DLL.

Ahora solo guarda el ensamblaje manteniendo la configuración de MDToken.

Inyección realizada, ahora lo unico que falta es hacer las configuraciones pertinentes para depurar/parchear :D

Feliz Reversing! :D

