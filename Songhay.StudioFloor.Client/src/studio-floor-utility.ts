import { DomUtility, WindowAnimation } from 'songhay';

export class StudioFloorUtility {
    static runMyAnimation(instance: DotNet.DotNetObject) : void {
        WindowAnimation.registerAndGenerate(1, async animation => {
            const x: number | null = await instance.invokeMethodAsync('getNextX');

            console.info(animation.getDiagnosticStatus());

            if(!x || x > 99) {
                animation.shouldStopAnimation = true;
            }
        });
    }
}

DomUtility.runWhenWindowContentLoaded(() => {
    console.info('the `DotNet` “namespace” should not be undefined:', {DotNet})

    console.warn({StudioFloorUtility}, {window});
});
