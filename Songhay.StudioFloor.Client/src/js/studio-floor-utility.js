import { __awaiter } from "tslib";
import { DomUtility, WindowAnimation } from 'songhay';
export class StudioFloorUtility {
    static runMyAnimation(instance) {
        console.warn({ instance });
        WindowAnimation.registerAndGenerate(1, (animation) => __awaiter(this, void 0, void 0, function* () {
            try {
                const x = yield instance.invokeMethodAsync('invokeAsync', null);
                console.warn({ x });
                console.info(animation.getDiagnosticStatus());
                if (!x || x > 99) {
                    WindowAnimation.cancelAnimation();
                }
            }
            catch (error) {
                console.error({ error });
                WindowAnimation.cancelAnimation();
            }
        }));
        WindowAnimation.animate();
    }
}
DomUtility.runWhenWindowContentLoaded(() => {
    console.info('the `DotNet` “namespace” should not be undefined:', { DotNet });
    console.warn({ StudioFloorUtility }, { window });
});
//# sourceMappingURL=studio-floor-utility.js.map