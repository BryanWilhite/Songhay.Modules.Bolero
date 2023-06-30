import { __awaiter } from "tslib";
import { DomUtility, WindowAnimation } from 'songhay';
export class StudioFloorUtility {
    static runMyAnimation(instance) {
        WindowAnimation.registerAndGenerate(1, (animation) => __awaiter(this, void 0, void 0, function* () {
            const x = yield instance.invokeMethodAsync('getNextX');
            console.info(animation.getDiagnosticStatus());
            if (!x || x > 99) {
                animation.shouldStopAnimation = true;
            }
        }));
    }
}
DomUtility.runWhenWindowContentLoaded(() => {
    console.info('the `DotNet` “namespace” should not be undefined:', { DotNet });
    console.warn({ StudioFloorUtility }, { window });
});
//# sourceMappingURL=studio-floor-utility.js.map