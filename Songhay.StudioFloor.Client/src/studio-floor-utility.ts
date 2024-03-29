import { DomUtility, WindowAnimation } from 'songhay';

export class StudioFloorUtility {
    static runMyAnimation(instance: DotNet.DotNetObject) : void {

        console.warn('runMyAnimation', {instance});

        WindowAnimation.registerAndGenerate(1, async animation => {
            try {
                const x: number | null = await instance.invokeMethodAsync('invokeAsync', null);

                console.warn('motionFunction:', {x});
    
                console.info(animation.getDiagnosticStatus());
    
                if(!x || x > 99) {
                    WindowAnimation.cancelAnimation();
                }
            } catch (error) {
                console.error({error});
                WindowAnimation.cancelAnimation();
            }
        });

        WindowAnimation.animate();
    }
}

DomUtility.runWhenWindowContentLoaded(() => {
    console.info('the `DotNet` “namespace” should not be undefined:', {DotNet})

    console.warn({StudioFloorUtility}, {window});
});
